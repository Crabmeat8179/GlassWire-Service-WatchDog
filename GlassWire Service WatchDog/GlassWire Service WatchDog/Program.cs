using System.Diagnostics;

namespace GlassWire_Service_WatchDog
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayAppContext());
        }
    }
    public class TrayAppContext : ApplicationContext
    {
        private NotifyIcon notifyIcon;
        public TrayAppContext()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = new Icon("icon.ico"), // Replace with your custom icon if you want
                Text = "GlassWire Service WatchDog",
                Visible = true
            };
            // Create a context menu
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Close", null, Exit_Click);
            // Assign the context menu to the NotifyIcon //CHATGPT generated Code
            notifyIcon.ContextMenuStrip = contextMenu;           
            Task.Run(() => WatchDog());
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && notifyIcon != null)
            {
                notifyIcon.Dispose();
            }
            base.Dispose(disposing);
        }

        public static bool GlassWireServiceRunning = false;

        public async Task WatchDog()
        {
            notifyIcon.BalloonTipTitle = "GlassWire Service WatchDog";
            notifyIcon.BalloonTipText = "Now Monitoring";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(3000);
            while (true)
            {
                GlassWireServiceRunning = false;
                Thread.Sleep(30000); //waits 30 seconds before checking again
                Process[] processes = Process.GetProcesses();
                foreach (Process program in processes)
                {
                    if (program.ProcessName == "GWCtlSrv")
                    {
                        GlassWireServiceRunning = true;
                        break;
                    }
                }
                if (GlassWireServiceRunning == false)
                {                                      
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = "C:\\Program Files (x86)\\GlassWire\\GWCtlSrv.exe";
                        process.StartInfo.Arguments = "-s";
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.Start();
                    }
                    notifyIcon.BalloonTipTitle = "GlassWire Service WatchDog";
                    notifyIcon.BalloonTipText = "The GlassWire Service has been restarted";
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.ShowBalloonTip(3000);

                    string[] DesktopFiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)); //get all files on the Desktop
                    foreach (string desktopFile in DesktopFiles)
                    {
                        if (desktopFile.Contains("GWCtlSrv"))
                        {
                            File.Delete(desktopFile);
                        }
                    }
                }
            }
        }
    }
}