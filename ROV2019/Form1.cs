using ROV2019.Models;
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
        public Main()
        {
            InitializeComponent();
            //read connections save file
            List<ArduinoConnection> connections = new List<ArduinoConnection>();
            ArduinoConnection test = new ArduinoConnection()
            {
                FriendlyName = "Hello World!"
            };
            connections.Add(test);
            //populate connections list box
            
            foreach(ArduinoConnection con in connections)
            {
                Control c = new Control();
                c.Text = con.FriendlyName;
                ConnectionsListFlow.Controls.Add(c);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

    }
}
