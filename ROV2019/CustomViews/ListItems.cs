﻿using ROV2019.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROV2019.CustomViews
{
    public class ConnectionListItem : GroupBox
    {
        public ConnectionListItem(ArduinoConnection con, EventHandler OnRemove, EventHandler OnTest, EventHandler OnSelect)
        {
            //Name Label
            Label name = new Label()
            {
                Text = con.FriendlyName,
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(name);
            //Remove Button
            Button removeButton = new Button()
            {
                Text = "Remove",
                Tag = con,
                Location = new Point(75, 25)
            };
            removeButton.Click += OnRemove;
            this.Controls.Add(removeButton);
        }
    }
}
