using CSCore.CoreAudioAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinVolumeMixer.Server
{
    class VolumeManager
    {
        private static VolumeManager instance = new VolumeManager();

        private MMDeviceEnumerator deviceEnum;
        private MMDevice device;
        private AudioSessionManager2 sessionManager;

        public static VolumeManager getManager()
        {
            return instance;
        }

        public void InitManager()
        {
            deviceEnum = new MMDeviceEnumerator();
            deviceEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            device = deviceEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            sessionManager = AudioSessionManager2.FromMMDevice(device);
        }

        public List<AudioSessionControl2> GetSessions()
        {
            List<AudioSessionControl2> sessions = new List<AudioSessionControl2>();
            foreach (var session in sessionManager.GetSessionEnumerator())
            {
                AudioSessionControl2 session2 = session.QueryInterface<AudioSessionControl2>();
                if (session2 != null && session2.Process != null && session2.Process.MainWindowTitle != null && session2.Process.MainWindowTitle != "")
                {
                    sessions.Add(session2);
                }
            }
            return sessions;
        }

        public AudioSessionControl2 GetSession(int processId)
        {
            foreach (AudioSessionControl2 session in instance.GetSessions())
            {
                if (session.Process.Id == processId)
                {
                    return session;
                }
            }
            return null;
        }

        public void SetVolume(int processId, float level)
        {
            AudioSessionControl2 session = instance.GetSession(processId);
            session.QueryInterface<SimpleAudioVolume>().MasterVolume = level;
        }

        public float GetVolume(int processId)
        {
            AudioSessionControl2 session = instance.GetSession(processId);
            return session.QueryInterface<SimpleAudioVolume>().MasterVolume;
        }

        public void SetMuted(int processId, bool muted)
        {
            AudioSessionControl2 session = instance.GetSession(processId);
            session.QueryInterface<SimpleAudioVolume>().IsMuted = muted;
        }

        public Boolean isMuted(int processId)
        {
            AudioSessionControl2 session = instance.GetSession(processId);
            return session.QueryInterface<SimpleAudioVolume>().IsMuted;
        }
    }
}