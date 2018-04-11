using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using TincControl.Properties;

namespace TincControl
{
    class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        Thread iconThread;

        public ProcessIcon()
        {
            ni = new NotifyIcon();
            ni.ContextMenuStrip = new ContextMenus().Create();

            iconThread = new Thread(Service_Thread);
            iconThread.SetApartmentState(ApartmentState.STA);
            iconThread.IsBackground = true;
            iconThread.Name = "Icon Worker";
            iconThread.Start();
        }

        public void Dispose()
        {
            ni.Dispose();
        }

        internal void Display()
        {
            ni.Visible = true;
        }

        private void Update_Icon(NotifyIcon ni, ServiceController sc, bool connected)
        {
            if (ni.ContextMenuStrip.InvokeRequired)
                ni.ContextMenuStrip.Invoke(new MethodInvoker(delegate { Update_Icon(ni, sc, connected); }));
            else
            {
                ni.Icon = connected ? Resources.connected : Resources.disconnected;
                ni.Text = $"{sc.ServiceName} {sc.Status.ToString()}";
                ni.ContextMenuStrip.Items[0].Enabled = !connected;
                ni.ContextMenuStrip.Items[1].Enabled = connected;
            }
        }

        private void Service_Thread()
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = Resources.ServiceName;
            while (true)
            {
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    Update_Icon(ni, sc, false);
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                }
                else if (sc.Status == ServiceControllerStatus.Running)
                {
                    Update_Icon(ni, sc, true);
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
        }
    }
}
