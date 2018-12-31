using ROV2019.Views;
using ROV2019.Models;
using ROV2019.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ROV2019.ControllerConfigurations;
using SlimDX.DirectInput;
using ROV2019.Controllers;
using System.Threading;

namespace ROV2019
{
    public partial class Main : Form
    {
        ConnectionManager connectionManager;
        ArduinoConnection selectedConnection = null;
        ConnectionContext openConnection = null;

        ConnectionControllerMesher mesher;

        ControllerManager ControllerManager;
        ControllerInfo SelectedController = null;

        public Main()
        {
            InitializeComponent();

            connectionManager = new ConnectionManager();
            ControllerManager = new ControllerManager();

            PopulateConnectionsList();
            PopulateControllersList();

            Thread controllerCheckerThread = new Thread(UpdateControllerConnectedStatus);
            controllerCheckerThread.IsBackground = true;
            controllerCheckerThread.Start();
        }

        private void UpdateControllerConnectedStatus()
        {
            while (true)
            {
                if (IsHandleCreated)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        foreach (Control c in ControllersListTable.Controls)
                        {
                            ControllerInfo info = (ControllerInfo)c.Tag;
                            if (!ControllerManager.IsControllerConnected(info))
                            {
                                c.BackColor = Color.Orange;
                            }
                            else
                            {
                                c.BackColor = Color.Transparent;
                            }
                        }

                    }));
                }
                Thread.Sleep(250);
            }
        }

        private void PopulateConnectionsList()
        {
            ConnectionsList.Controls.Clear();
            //populate connections list table
            foreach (ArduinoConnection con in connectionManager.SavedConnections)
            {
                ConnectionListItem connectionItem = new ConnectionListItem(con, RemoveButton_Click, null, ConnectionSelected);
                if (selectedConnection == con)
                    connectionItem.ToggleSelected();
                ConnectionsList.Controls.Add(connectionItem, 0, ConnectionsList.RowCount - 1);
            }
        }

        private void PopulateControllersList()
        {
            ControllersListTable.Controls.Clear();

            foreach (ControllerInfo info in ControllerManager.SavedControllers)
            {
                ControllerListItem listItem = new ControllerListItem(info, RemoveController, null, SelectController);
                if (info == SelectedController)
                    listItem.ToggleSelected();
                if (!ControllerManager.IsControllerConnected(info))
                    listItem.BackColor = Color.Orange;
                ControllersListTable.Controls.Add(listItem, 0, ControllersListTable.RowCount - 1);
            }
        }


        private void SetConnectionSpecificButtonsEnabled(bool enabled)
        {
            ConnectButton.Enabled = enabled;
            ConfigureTrimButton.Enabled = enabled;
            SensorButton.Enabled = enabled;
        }

        private void setConnectionListItemDisplayConnected(bool connected)
        {
            ConnectionListItem listItem = (ConnectionListItem)ConnectionsList.Controls.Find(selectedConnection.FriendlyName + selectedConnection.IpAddress, true)[0];
            if (connected)
            {
                listItem.BackColor = Color.Green;
                listItem.Click -= ConnectionSelected;
                listItem.Cursor = Cursors.Arrow;
            }
            else
            {
                listItem.BackColor = Color.Yellow;
                listItem.Click += ConnectionSelected;
                listItem.Cursor = Cursors.Hand;
            }
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mesher != null && mesher.IsMeshing)
            {
                mesher.StopMesh();
            }
            if(openConnection != null && openConnection.isConnected())
            {
                openConnection.Close();
            }
        }


        #region Click Handlers

        private void PIDAssistField_CheckedChanged(object sender, EventArgs e)
        {
            if (mesher != null)
            {
                mesher.IsUsingPID = ((CheckBox)sender).Checked;
            }
        }

        private void RemoveController(object sender, EventArgs e)
        {
            if (mesher == null || !mesher.IsMeshing)
            {
                ControllerInfo info = (ControllerInfo)((Button)sender).Tag;
                ControllerManager.Remove(info);
                PopulateControllersList();
            }
        }

        private void SelectController(object sender, EventArgs e)
        {
            ControllerListItem listItem = (ControllerListItem)sender;
            if (listItem.Selected && mesher == null)
            {
                //un-select
                UseControllerButton.Enabled = listItem.ToggleSelected();
                SelectedController = null;
            }
            else if (SelectedController == null)
            {
                //select
                SelectedController = (ControllerInfo)listItem.Tag;
                UseControllerButton.Enabled = listItem.ToggleSelected();
            }
        }

        private void ConnectionSelected(object sender, EventArgs e)
        {
            ConnectionListItem selectedConn = (ConnectionListItem)sender;
            if (selectedConn.Selected)
            {
                //unselecting
                this.selectedConnection = null;
                SetConnectionSpecificButtonsEnabled(selectedConn.ToggleSelected());
            }
            else
            {
                //selecting
                //make sure one is not already selected
                if (this.selectedConnection == null)
                {
                    SetConnectionSpecificButtonsEnabled(true);
                    this.selectedConnection = (ArduinoConnection)selectedConn.Tag;
                    selectedConn.ToggleSelected();
                }
            }

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            ArduinoConnection conn = (ArduinoConnection)((Button)sender).Tag;
            if (openConnection == null || openConnection.connection != conn)
            {
                connectionManager.Remove(conn);
                if (conn == selectedConnection)
                    selectedConnection = null;
                PopulateConnectionsList();
            }
        }

        private void manualAddButton_Click(object sender, EventArgs e)
        {
            new ManualConnectionAddDialog(connectionManager);
            PopulateConnectionsList();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.Equals("Connect"))
            {
                this.openConnection = new ConnectionContext(this.selectedConnection);
                if (openConnection.OpenConnection())
                {
                    //check authorization
                    string userPassword = null;
                    //TODO: Figure out if API will close socket or not.
                    while (!openConnection.Authorize())
                    {
                        //prompt user for correct password
                        userPassword = Dialog.ShowPrompt("Password Please:", selectedConnection.Password);
                        selectedConnection.Password = userPassword;
                        openConnection.connection = selectedConnection;
                        connectionManager.Save(selectedConnection);
                        if (userPassword.Equals(""))
                            return;
                    }
                    //TODO: Refactor maybe
                    button.Text = "Disconnect";
                    setConnectionListItemDisplayConnected(true);
                }
                else
                {
                    Dialog.ShowMessageDialog("Error Opening Connection to ROV. Most likely Connection Refused.");
                }
            }
            else
            {
                openConnection.Close();
                button.Text = "Connect";
                //TODO: Refactor
                setConnectionListItemDisplayConnected(false);
            }
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            Progress<ArduinoConnection> discoverConnectionListener = new Progress<ArduinoConnection>(connection =>
            {
                PopulateConnectionsList();
            });
            ConnectionScanProgressDialog scanProgressDialog = new ConnectionScanProgressDialog(connectionManager, discoverConnectionListener);
            scanProgressDialog.Show();
        }

        private void ConfigureTrimButton_Click(object sender, EventArgs e)
        {
            TrimConfigurationDialog configurationDialog = new TrimConfigurationDialog(connectionManager, selectedConnection);
        }

        private void SensorButton_Click(object sender, EventArgs e)
        {
            //TODO: Open Dialog with senor data.
        }

        private void AddControllerButton_Click(object sender, EventArgs e)
        {
            //TODO: Show add controller dialog.
            ControllerInfo newController = new ControllerInfo()
            {
                FriendlyName = "Testing",
                ControllerClass = "PlayStation",
                ConfigurationClass = typeof(Arcade).Name,
                Type = ControllerType.SlimDX
            };
            ControllerManager.Add(newController);
            PopulateControllersList();
        }

        private void UseControllerButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.Contains("Stop"))
            {

                button.Text = "Use";
                if(mesher != null)
                {
                    mesher.StopMesh();
                }
            }
            else
            {

                if (openConnection != null && openConnection.isConnected() && ControllerManager.IsControllerConnected(SelectedController))
                {
                    //start the mesh

                    //read what type of configuration to use from saved properties.
                    ControllerConfiguration config = ControllerManager.GetConfiguration(SelectedController);
                    mesher = new ConnectionControllerMesher(openConnection, config);
                    mesher.StartMesh();
                    UseControllerButton.Text = "Stop Using";

                }
            }
        }

        #endregion

        
    }
}
