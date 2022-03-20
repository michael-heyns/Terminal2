using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Terminal
{
    class Log
    {
        private static string _filename = string.Empty;
        private static bool _enabled = false;

        public static string Filename
        {
            get { return _filename; }
        }

        public static void Start(string filename)
        {
            if (_enabled)
                return;

            _filename = filename;

            AssemblyInfo info = new AssemblyInfo();
            string text = $"\r\n";
            text += $"# -----------------------------------------------------\r\n";
            text += $"# {info.Title} - v{info.AssemblyVersion} - {info.Copyright}\r\n";
            text += $"# {DateTime.Now}\r\n";
            text += $"# -----------------------------------------------------\r\n";
            text += $"\r\n";
            Add(text);
            _enabled = true;
        }

        public static void Stop()
        {
            string text = $"\r\n";
            text += $"# -- END ----------------------------------------------\r\n";
            Add(text);
            _enabled = false;
        }

        public static bool Enabled
        {
            get { return _enabled; }
        }

        public static void Add(string text)
        {
            if (!_enabled || _filename.Length == 0)
                return;
            for (int retry = 0; retry < 5; retry++)
            {
                try
                {
                    File.AppendAllText(_filename, text);
                    return;
                }
                catch { }
            }
        }
    }
}
