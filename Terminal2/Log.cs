/* 
 * Terminal2
 *
 * Copyright © 2022-23 Michael Heyns
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Terminal
{
    class Log
    {
        private static string _directory;
        private static string _prefix;
        private static int _maxLength = 0;
        private static int _currentLength = 0;

        private static string _filename = string.Empty;
        private static bool _enabled = false;

        static SemaphoreSlim semaphore = new SemaphoreSlim(0); // Starts at 0, so waiting thread blocks
        static Queue<string> strings = new Queue<string>();
        static Thread workerThread = null;
        static readonly object locker = new object();

        public static string Filename
        {
            get { return _filename; }
        }

        static bool _WriteToFile(string text)
        {
            bool reply = true;
            for (int retry = 0; retry < 5; retry++)
            {
                try
                {
                    File.AppendAllText(_filename, text);

                    if (_maxLength > 0)
                    {
                        _currentLength += text.Length;
                        if (_currentLength >= _maxLength)
                        {
                            _currentLength = 0;

                            string txt = $"\r\n";
                            txt += $"# -- END ----------------------------------------------\r\n";
                            File.AppendAllText(_filename, txt);

                            _filename = _directory + @"\" + _prefix + Utils.TimestampForFilename() + ".log";
                            reply = true;

                            AssemblyInfo info = new AssemblyInfo();
                            txt = $"\r\n";
                            txt += $"# -----------------------------------------------------\r\n";
                            txt += $"# {info.Title} - v{info.AssemblyVersion} - {info.Copyright}\r\n";
                            txt += $"# {DateTime.Now}\r\n";
                            txt += $"# -----------------------------------------------------\r\n";
                            txt += $"\r\n";
                            File.AppendAllText(_filename, txt);
                        }
                    }
                    return reply;
                }
                catch { }
            }
            return false;

        }

        static void LogThread()
        {
            while (true)
            {
                semaphore.Wait();

                lock (locker)
                {
                    while (strings.Count > 0)
                    {
                        string text = strings.Dequeue();
                        _WriteToFile(text);
                    }
                }
            }
        }

        public static void Start(string directory, string prefix, int maxLength)
        {
            if (_enabled)
                return;

            _directory = directory;
            _maxLength = maxLength;
            _prefix = prefix;
            _currentLength = 0;

            _filename = _directory + @"\" + _prefix + Utils.TimestampForFilename() + ".log";

            // Create the worker thread that loops forever waiting for data to log
            workerThread = new Thread(LogThread);
            workerThread.IsBackground = true; // Makes the thread background so it dies when the main program exits
            workerThread.Start();

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
            if (workerThread != null)
            {
                string text = $"\r\n";
                text += $"# -- END ----------------------------------------------\r\n";
                Add(text);

                while (strings.Count > 0)
                    Thread.Sleep(10);

                workerThread.Abort();
                workerThread.Join();
                workerThread = null;
            }
            _enabled = false;
        }

        public static bool Enabled
        {
            get { return _enabled; }
        }

        public static bool Add(string text)
        {
            if (!_enabled || _filename.Length == 0)
                return false;
            lock (locker)
            {
                strings.Enqueue(text);
            }
            semaphore.Release();
            return true;
        }
    }
}
