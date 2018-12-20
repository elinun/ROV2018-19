﻿using ROV2019.CustomViews;
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
                ConnectionListItem connectionItem = new ConnectionListItem(con, RemoveButton_Click, null, null);
                ConnectionsList.Controls.Add(connectionItem, 0, ConnectionsList.RowCount);
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
            connectionManager.Save(newConn);
            PopulateConnectionsList();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {

        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            //TODO: Open Progress Dialog Form
        }
    }
}