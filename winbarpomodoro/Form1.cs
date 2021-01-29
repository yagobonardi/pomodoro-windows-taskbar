using System;
using System.Windows.Forms;

namespace winbarpomodoro
{
    public partial class Form1 : Form
    {
        private DateTime StartHour { get; set; }
        private DateTime StoptHour { get; set; }

        private int Cycle { get; set; }

        public Form1()
        {
            InitializeComponent();

            Cycle = 0;

            btnStop.Enabled = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();

                notifyIcon.Visible = true;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            Cycle++;

            notifyIcon.Icon = Properties.Resources.red;
            notifyIcon.Text = "stay focused!";

            this.WindowState = FormWindowState.Minimized;

            StartHour = DateTime.Now;

            StoptHour = StartHour.AddMinutes(25);

            while (DateTime.Now <= StoptHour) ;

            btnStop.Enabled = true;

            notifyIcon.Icon = Properties.Resources.green;
            notifyIcon.Text = "cycle ended";
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetMessage();

            this.Show();

            this.WindowState = FormWindowState.Normal;

            this.notifyIcon.Visible = false;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            notifyIcon.Icon = Properties.Resources.blue;
            notifyIcon.Text = "just rest";

            this.WindowState = FormWindowState.Minimized;

            var restStart = DateTime.Now;

            var minutesToAdd = 0;

            if (Cycle == 4) { Cycle = 0; minutesToAdd = 15; } else { minutesToAdd = 5; }

            var restStop = restStart.AddMinutes(minutesToAdd);

            while (DateTime.Now < restStop) ;

            label1.Text = "start a cycle!";

            this.Show();

            this.WindowState = FormWindowState.Normal;

            btnStart.Enabled = true;

            btnStop.Enabled = false;
        }

        private void SetMessage()
        {
            switch (Cycle)
            {
                case 1:
                    label1.Text = "First cycle concluded, take five minutes of rest";
                    break;

                case 2:
                    label1.Text = "Second cycle concluded, take five minutes of rest";
                    break;

                case 3:
                    label1.Text = "Third cycle concluded, take five minutes of rest";
                    break;

                case 4:
                    label1.Text = "Fourth cycle concluded! take 15~30 min of rest";
                    break;

                default:
                    label1.Text = "start a cycle!";
                    break;
            }
        }
    }
}