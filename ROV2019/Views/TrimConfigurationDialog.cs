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

namespace ROV2019.Views
{
    public partial class TrimConfigurationDialog : Form
    {

        ConnectionManager connectionManager;
        ArduinoConnection connectionProperties;

        public TrimConfigurationDialog(ConnectionManager manager, ArduinoConnection connection)
        {
            InitializeComponent();

            connectionManager = manager;
            connectionProperties = connection;
            ShowDialog();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            
            Close();
        }
    }
}
