using System;
using System.Security.Principal;
using System.Windows.Forms;
using TincControl.Properties;

namespace TincControl
{
    static class Program
    {
        [STAThread]
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            if (!IsAdministrator())
            {
                MessageBox.Show($"Process needs to run with Administrator privileges to manage {Resources.ServiceName}",
                    Resources.ServiceName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
#endif
            {
                using (ProcessIcon pi = new ProcessIcon())
                {
                    pi.Display();
                    Application.Run();
                }
            }
        }
    }
}
