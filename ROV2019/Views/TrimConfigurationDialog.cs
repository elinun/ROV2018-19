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

        public TrimConfigurationDialog(ConnectionManager manager)
        {
            InitializeComponent();

            connectionManager = manager;
            ShowDialog();
        }
    }
}
