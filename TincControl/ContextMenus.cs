using System;
using System.ServiceProcess;
using System.Windows.Forms;
using TincControl.Properties;

namespace TincControl
{
    class ContextMenus
    {
        public ContextMenuStrip Create()
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = Resources.ServiceName;

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator separator;

            item = new ToolStripMenuItem();
            item.Click += new EventHandler((sender, e) => Start_Click(sender, e, sc));
            item.Text = $"Start {sc.ServiceName}";
            menu.Items.Add(item);

            item = new ToolStripMenuItem();
            item.Click += new EventHandler((sender, e) => Stop_Click(sender, e, sc));
            item.Text = $"Stop {sc.ServiceName}";
            menu.Items.Add(item);

            separator = new ToolStripSeparator();
            menu.Items.Add(separator);

            item = new ToolStripMenuItem();
            item.Click += new EventHandler(Exit_Click);
            item.Text = "Exit";
            menu.Items.Add(item);

            return menu;
        }

        private void Start_Click(object sender, EventArgs e, ServiceController sc)
        {
            try
            {
                sc.Start();
            }
            catch
            {
                MessageBox.Show($"Cannot manage {sc.ServiceName}. No Administrator privileges?",
                    sc.ServiceName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Stop_Click(object sender, EventArgs e, ServiceController sc)
        {
            try
            {
                sc.Stop();
            }
            catch
            {
                MessageBox.Show($"Cannot manage {sc.ServiceName}. No Administrator privileges?",
                    sc.ServiceName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
