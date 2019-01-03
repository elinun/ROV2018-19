using ROV2019.Presenters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROV2019.Views
{
    public static class Dialog
    {
        public static string ShowPrompt(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static void ShowMessageDialog(string message)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Message",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = message, AutoSize = true };
            prompt.Controls.Add(textLabel);
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.AcceptButton = confirmation;
            prompt.ShowDialog();
        }
    }

    public class AttitudeIndicator : Control
    {
        public int RollMax { get; set; } = 255;
        public int RollMin { get; set; } = -255;
        private int rollValue;
        public int RollValue { get { return this.rollValue; } set { this.rollValue = value; Invalidate(); } }

        public int PitchMax { get; set; } = 255;
        public int PitchMin { get; set; } = 255;
        private int pitchValue;
        public int PitchValue { get { return this.pitchValue; } set { this.pitchValue = value;  Invalidate(); } }

        private int pixelRange;
        private int cX;
        private int cY;

        protected override void OnPaint(PaintEventArgs e)
        {
            pixelRange = Math.Min(Size.Height, Size.Width);
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            //draw overlaying circle
            g.DrawArc(new Pen(Color.Black), 0, 0, pixelRange, pixelRange, 0, 360);
            //draw upper part
            Pen topPart = new Pen(Color.Blue);
            //determine points from ROll and Pitch
            //mapped to pixelRange * [0, 1]
            double xPercent = (((RollValue - RollMin) / (double)(RollMax - RollMin)));
            double yPercent = (((PitchValue - PitchMin) / (double)(PitchMax - PitchMin)) - 0.0);
            cX = (int)(pixelRange * xPercent);
            cY = (int)(pixelRange * yPercent);
            g.FillRectangle(new Pen(Color.Black).Brush, cX, cY, 10, 10);
            Point arcStart = new Point((int)cX+(int)(Math.Sin(Math.PI * xPercent) * ((pixelRange-cX) / 2.5)), cY + (int)(Math.Cos(Math.PI * xPercent) * ((pixelRange-cY) / 2.5)));
            g.FillRectangle(new Pen(Color.Black).Brush, arcStart.X, arcStart.Y, 10, 10);
            Point[] topPoints = {
                //start
                

                //end
                new Point()
            };
            //g.FillClosedCurve(topPart.Brush, topPoints);
        }
    }
}
