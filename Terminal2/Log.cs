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
        private static string _fileData = string.Empty;

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
            _fileData = $"\n";
            _fileData += $"# -----------------------------------------------------\n";
            _fileData += $"# {info.Title} - v{info.AssemblyVersion} - {info.Copyright}\n";
            _fileData += $"# {DateTime.Now}\n";
            _fileData += $"# -----------------------------------------------------\n";
            _fileData += $"\n";
            _enabled = true;
        }

        public static void Stop()
        {
            _fileData = $"\n";
            _fileData += $"# -- END ----------------------------------------------\n";
            Flush();
            _enabled = false;
        }

        public static bool Enabled
        {
            get { return _enabled; }
        }

        public static void Add(string line)
        {
            if (!_enabled || _filename.Length == 0)
                return;
            lock (_fileData)
            {
                _fileData += line;
                _fileData += "\n";
            }
        }

        public static void Flush()
        {
            if (!_enabled || _filename.Length == 0)
                return;
            lock (_fileData)
            {
                if (_fileData.Length == 0)
                    return;
                for (int retry = 0; retry < 5; retry++)
                {
                    try
                    {
                        File.AppendAllText(_filename, _fileData);
                        _fileData = string.Empty;
                        return;
                    }
                    catch { }
                }
            }
        }
    }
}
