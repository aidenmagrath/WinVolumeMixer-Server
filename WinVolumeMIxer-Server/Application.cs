using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinVolumeMixer.Server
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Volume { get; set; }
        public bool Muted { get; set; }
    }
}