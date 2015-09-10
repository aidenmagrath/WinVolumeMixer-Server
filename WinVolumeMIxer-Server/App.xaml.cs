using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WinVolumeMixer.Server
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            VolumeManager.getManager().InitManager();
            TaskbarIcon tbi = (TaskbarIcon)FindResource("MyNotifyIcon");

            ApplicationManager.getManager().GetMenuItem("menuStart").IsEnabled = true;
            ApplicationManager.getManager().GetMenuItem("menuStop").IsEnabled = false;
        }
    }
}
