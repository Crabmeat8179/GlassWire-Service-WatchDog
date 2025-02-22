using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassWire_Service_WatchDog
{
    public partial class Interval_Changer_Window : Form
    {
        public Interval_Changer_Window()
        {
            InitializeComponent();
            this.Load += Interval_Changer_Window_Load;
        }

        private void Interval_Changer_Window_Load(object sender, EventArgs e)
        {
            string CurrentDelay = File.ReadAllText(Directory.GetCurrentDirectory() + "\\DelayInterval.txt");

            User_Inputted_time.Text = CurrentDelay.Substring(0,2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\DelayInterval.txt", User_Inputted_time.Text + "000");
            //MessageBox.Show("Changed File");
            //MessageBox.Show(Process.GetCurrentProcess().ProcessName);
            //Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\{Process.GetCurrentProcess().ProcessName}.exe");
            Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\{Process.GetCurrentProcess().ProcessName}.exe"));
            Environment.Exit(0);
        }
    }
}
