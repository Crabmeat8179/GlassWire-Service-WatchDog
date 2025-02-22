using System.Diagnostics;
using System.Reflection;

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

            if (!File.Exists(Directory.GetCurrentDirectory() + "\\DelayInterval.txt"))
            {
                //MessageBox.Show("Hit Delay not found");
                File.Create(Directory.GetCurrentDirectory() + "\\DelayInterval.txt").Close();
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\DelayInterval.txt", "30000"); //30 second delay by default
            }
            
            notifyIcon = new NotifyIcon
            {
                Icon = new Icon("icon.ico"), // Replace with your custom icon if you want
                Text = "GlassWire Service WatchDog",
                Visible = true
            };
            // Create a context menu
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Close", null, Exit_Click);
            contextMenu.Items.Add("Change interval", null, ChangeInterval);
            // Assign the context menu to the NotifyIcon //CHATGPT generated Code
            notifyIcon.ContextMenuStrip = contextMenu;
            string DelayFromFile = File.ReadAllText(Directory.GetCurrentDirectory() + "\\DelayInterval.txt");
            //MessageBox.Show($"Using delay {DelayFromFile}");
            //MessageBox.Show(pathtyest);
            Task.Run(() => WatchDog(DelayFromFile));
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Application.Exit();
        }
        
        private void ChangeInterval(object sender, EventArgs e)
        {
            //bring up a small window where you can chnage how long the interval of waiting is for the program to rescan if the service is running
            Interval_Changer_Window interval_Changer_Window = new Interval_Changer_Window();
            interval_Changer_Window.Show();
            interval_Changer_Window.BringToFront();
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

        public async Task WatchDog(string delay)
        {
            notifyIcon.BalloonTipTitle = "GlassWire Service WatchDog";
            notifyIcon.BalloonTipText = "Now Monitoring";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(3000);
            
            while (true)
            {
                GlassWireServiceRunning = false;
                Thread.Sleep(Int32.Parse(delay)); 
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