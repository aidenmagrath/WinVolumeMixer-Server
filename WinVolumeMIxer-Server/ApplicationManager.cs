﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.CoreAudioAPI;
using Microsoft.Owin.Hosting;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
using WinVolumeMixer.Server.Properties;

namespace WinVolumeMixer.Server
{
    class ApplicationManager
    {
        private static ApplicationManager instance = new ApplicationManager();

        private List<Application> applications = new List<Application>();

        IDisposable webAppDisposible;

        public static ApplicationManager getManager()
        {
            return instance;
        }

        public void StartWebApp()
        {
            UpdateApplications();
            webAppDisposible = WebApp.Start<Startup>(url: "http://localhost:9000/");


            GetMenuItem("menuStart").IsEnabled = false;
            GetMenuItem("menuStop").IsEnabled = true;

            GetTaskbarIcon().Icon = Resources.Icon_On;
            GetTaskbarIcon().ToolTipText = "WinVolumeMixer (Running)";
        }

        public void StopWebApp()
        {
            if (webAppDisposible != null)
            {
                webAppDisposible.Dispose();
            }

            GetMenuItem("menuStart").IsEnabled = true;
            GetMenuItem("menuStop").IsEnabled = false;

            GetTaskbarIcon().Icon = Resources.Icon_Off;
            GetTaskbarIcon().ToolTipText = "WinVolumeMixer (Stopped)";
        }

        public TaskbarIcon GetTaskbarIcon()
        {
            return (TaskbarIcon)GetWpfApp().FindResource("MyNotifyIcon");
        }

        public System.Windows.Application GetWpfApp()
        {
            return System.Windows.Application.Current;
        }

        public MenuItem GetMenuItem(string name)
        {
            ContextMenu menu = GetTaskbarIcon().ContextMenu;
            foreach (MenuItem item in menu.Items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }

        public void AddApplication(Application app)
        {
            foreach (Application a in applications)
            {
                if (a.Id == app.Id)
                {
                    return;
                }
            }
            applications.Add(app);
        }

        public void RemoveApplication(Application app)
        {
            if (applications.Contains(app))
            {
                applications.Remove(app);
            }
        }

        public void ClearApplications()
        {
            applications.Clear();
        }

        public Application GetApplication(int processId)
        {
            foreach (Application app in applications)
            {
                if (app.Id == processId)
                {
                    return app;
                }
            }

            return null;
        }

        public List<Application> GetApplications()
        {
            return applications;
        }

        public void UpdateApplications()
        {
            getManager().ClearApplications();
            foreach (AudioSessionControl2 session in VolumeManager.getManager().GetSessions())
            {
                int id = session.Process.Id;
                string name = session.Process.MainWindowTitle;
                float volume = VolumeManager.getManager().GetVolume(id);
                bool muted = VolumeManager.getManager().isMuted(id);
                getManager().AddApplication(new Application { Id = id, Name = name, Volume = volume, Muted = muted });
            }
        }

    }
}