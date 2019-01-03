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

            //populate sliders
            ForwardTrackBar.Value = connection.Trim.LeftToRightCorrection;
            YawTrackBar.Value = connection.Trim.FrontToBackCorrection;
            RollTrackBar.Value = connection.Trim.RollCorrection;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            connectionManager.Save(connectionProperties);
            Close();
        }

        private void ForwardTrackBar_Scroll(object sender, EventArgs e)
        {
            connectionProperties.Trim.LeftToRightCorrection = ForwardTrackBar.Value;
        }

        private void YawTrackBar_Scroll(object sender, EventArgs e)
        {
            connectionProperties.Trim.FrontToBackCorrection = YawTrackBar.Value;
        }

        private void RollTrackBar_Scroll(object sender, EventArgs e)
        {
            connectionProperties.Trim.RollCorrection = RollTrackBar.Value;
        }
    }
}
