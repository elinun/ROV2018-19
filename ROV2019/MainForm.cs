using ROV2019.CustomViews;
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

namespace ROV2019
{
    public partial class Main : Form
    {
        ConnectionManager connectionManager;
        ArduinoConnection selectedConnection = null;
        ConnectionContext openConnection = null;

        public Main()
        {
            InitializeComponent();

            connectionManager = new ConnectionManager();

            PopulateConnectionsList();
            
        }

        private void PopulateConnectionsList()
        {
            ConnectionsList.Controls.Clear();
            //populate connections list table
            foreach (ArduinoConnection con in connectionManager.SavedConnections)
            {
                ConnectionListItem connectionItem = new ConnectionListItem(con, RemoveButton_Click, null, ConnectionSelected);
                ConnectionsList.Controls.Add(connectionItem, 0, ConnectionsList.RowCount - 1);
            }
        }

        private void ConnectionSelected(object sender, EventArgs e)
        {
            ConnectionListItem selectedConn = (ConnectionListItem)sender;
            if (selectedConn.Selected)
            {
                //unselecting
                this.selectedConnection = null;
                ConnectButton.Enabled = false;
                ConfigureTrimButton.Enabled = false;
                selectedConn.ToggleSelected();
            }
            else
            {
                //selecting
                //make sure one is not already selected
                if(this.selectedConnection == null)
                {
                    ConnectButton.Enabled = true;
                    ConfigureTrimButton.Enabled = true;
                    this.selectedConnection = (ArduinoConnection)selectedConn.Tag;
                    selectedConn.ToggleSelected();
                }
            }
            
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            connectionManager.Remove((ArduinoConnection)((Button)sender).Tag);
            PopulateConnectionsList();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void manualAddButton_Click(object sender, EventArgs e)
        {
            //TODO: Open dialog
            ArduinoConnection newConn = new ArduinoConnection()
            {
                FriendlyName = "Hi"
            };
            connectionManager.Add(newConn);
            PopulateConnectionsList();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(button.Text.Equals("Connect"))
            {
                this.openConnection = new ConnectionContext(this.selectedConnection);
                openConnection.OpenConnection();
                //check authorization
                string userPassword = null;
                while (!openConnection.Authorize())
                {
                    //prompt user for correct password
                    userPassword = Prompt.ShowDialog("Password Please:", selectedConnection.Password);
                    selectedConnection.Password = userPassword;
                    openConnection.connection = selectedConnection;
                    connectionManager.Save(selectedConnection);
                    if (userPassword.Equals(""))
                        return;
                }
                button.Text = "Disconnect";
            }
            else
            {
                openConnection.Close();
                button.Text = "Connect";
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
            //TODO: Open ConfigureTrim Dialog, and update associated models
        }
    }
}
