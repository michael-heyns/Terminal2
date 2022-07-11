/* 
 * Terminal2
 *
 * Copyright © 2022 Michael Heyns
 * 
 * This file is part of Terminal2.
 * 
 * Terminal2 is free software: you  can redistribute it and/or  modify it 
 * under the terms of the GNU General Public License as published  by the 
 * Free Software Foundation, either version 3 of the License, or (at your
 * option) any later version.
 * 
 * Terminal2 is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the  implied  warranty  of MERCHANTABILITY or 
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
 * more details.
 * 
 * You should have  received a copy of the GNU General Public License along 
 * with Terminal2. If not, see <https://www.gnu.org/licenses/>. 
 *
 */


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
