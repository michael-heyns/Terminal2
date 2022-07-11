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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.Xml.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace Terminal
{
    internal class Database
    {
        private enum FSection { None, Profile, Connections, Macros };
        private static readonly string _directory = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Terminal2";
        private const int SEARCH_INDEX = 11;

        public static void Initialise()
        {
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            Directory.SetCurrentDirectory(_directory);
        }

        public static void Backup()
        {
            string lastDay = string.Empty;
            string lastDayFilename = _directory + $"\\Backup";
            if (File.Exists(lastDayFilename))
            {
                try
                {
                    lastDay = File.ReadAllText(lastDayFilename);
                }
                catch { }
            }

            string today = DateTime.Now.DayOfWeek.ToString();

            if (lastDay != today)
            {
                string backupDir = _directory + $"\\{today}";
                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                if (Directory.Exists(backupDir))
                {
                    try
                    {
                        var dir = Directory.EnumerateFiles(_directory);
                        foreach (string s in dir)
                        {
                            if (s.EndsWith(".profile"))
                            {
                                string name = Path.GetFileNameWithoutExtension(s);
                                string newName = backupDir + $"\\{name}.profile";
                                if (File.Exists(newName))
                                    File.Delete(newName);

                                if (!File.Exists(newName))
                                    File.Copy(s, newName);
                            }
                        }
                        File.WriteAllText(lastDayFilename, today);
                    }
                    catch { }
                }
            }
        }

        public static bool Find(string name)
        {
            try
            {
                var dir = Directory.EnumerateFiles(_directory);
                foreach (string s in dir)
                {
                    if (s.EndsWith(".profile"))
                    {
                        string n = Path.GetFileNameWithoutExtension(s);
                        if (n.Equals(name))
                        {
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        public static bool Rename(string oldname, string newname)
        {
            string oldf = _directory + $"\\{oldname}.profile";
            string newf = _directory + $"\\{newname}.profile";

            try
            {
                if (File.Exists(oldf) && !File.Exists(newf))
                {
                    File.Copy(oldf, newf);
                    File.Delete(oldf);
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static bool Copy(string srcname, string dstname)
        {
            string srcf = _directory + $"\\{srcname}.profile";
            string dstf = _directory + $"\\{dstname}.profile";

            try
            {
                if (File.Exists(srcf) && !File.Exists(dstf))
                {
                    File.Copy(srcf, dstf);
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static bool Export(string srcname, string fullpath)
        {
            string srcf = _directory + $"\\{srcname}.profile";

            try
            {
                if (File.Exists(srcf))
                {
                    File.Delete(fullpath);
                    File.Copy(srcf, fullpath);
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static bool Import(string fullpath)
        {
            string name = Path.GetFileNameWithoutExtension(fullpath);
            string dstf = _directory + $"\\{name}.profile";

            try
            {
                if (File.Exists(dstf))
                {
                    MessageBox.Show($"The profile '{name}' already exists", "Duplicate Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    if (fullpath.ToLower().EndsWith(".tmf"))
                    {
                        Database.NewProfile(name);
                        string[] lines = File.ReadAllLines(fullpath);
                        if (!lines[0].StartsWith("# Terminal macro file v2"))
                        {
                            MessageBox.Show($"The TMF file is not valid", "Invalid File Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        string data = "\n";
                        int number = 0;
                        for (int i = 1; i < lines.Length; i += 2)
                        {
                            string title = lines[i];
                            if (i < lines.Length - 1)
                            {
                                string macro = lines[i + 1];

                                data += $"[{number++}]\n";
                                data += $"Title={title}\n";
                                data += "ICD=0\n";
                                data += "ILD=0\n";
                                data += "RepeatON=False\n";
                                data += "Delta=0\n";
                                data += "Repeats=0\n";
                                if (macro.EndsWith("$0D"))
                                {
                                    data += $"Text={macro.Substring(0, macro.Length - 3)}\n";
                                    data += "AddCR=True\n";
                                }
                                else
                                {
                                    data += $"Text={macro}\n";
                                    data += "AddCR=False\n";
                                }
                                data += "AddLF=False\n";

                                string filename = _directory + $"\\{name}.profile";
                                File.AppendAllText(filename, data);
                            }
                        }
                        string currentExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                        System.Diagnostics.Process.Start(currentExecutable, "\"" + name + "\"");
                    }
                    else if (File.Exists(fullpath))
                    {
                        File.Copy(fullpath, dstf);
                    }
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static void GetAllNames(ListBox listBox)
        {
            listBox.Items.Clear();
            try
            {
                var dir = Directory.EnumerateFiles(_directory);
                foreach (string s in dir)
                {
                    if (s.EndsWith(".profile"))
                    {
                        string n = Path.GetFileNameWithoutExtension(s);
                        listBox.Items.Add(n);
                    }
                }
            }
            catch { }
        }

        public static void Remove(string name)
        {
            string fname = _directory + $"\\{name}.profile";
            try
            {
                if (File.Exists(fname))
                {
                    File.Delete(fname);
                }
            }
            catch { }
        }

        public static bool SaveProfile(Profile profile)
        {
            string filename = _directory + $"\\{profile.name}.profile";
            return SaveProfile(profile, filename);
        }

        public static bool NewProfile(string name)
        {
            try
            {
                string data = string.Empty;
                data += "# -----------------------------------------------------\n";
                data += "# Terminal2 - Copyright ©2022 Michael Heyns\n";
                data += $"# New profile for {name}\n";
                data += "# -----------------------------------------------------\n";
                data += "\n";
                data += "[Profile]\n";
                data += $"Name={name}\n";
                data += "SendCR=True\n";
                data += "SendLF=False\n";
                data += "ClearCMD=False\n";
                data += "OnTop=False\n";
                data += "TimeOutput=True\n";
                data += "MaxLines=2000\n";
                data += "CutLines=500\n";
                data += "ShowCR=False\n";
                data += "ShowLF=False\n";
                data += "ShowASCII=True\n";
                data += "ShowHEX=False\n";
                data += "TimeInput=True\n";
                data += "LogDir=C:\\Temp\n";
                data += "LogPrefix=Default_\n";
                data += "OutFont=Courier New, 8.25pt\n";
                data += "InFont=Courier New, 11.25pt\n";
                data += "# Input panel colours\n";
                data += "InBackColor=-1\n";
                data += "InTextColor=-16777216\n";
                data += "# Output panel colours\n";
                data += "OutBackColor=-2302756\n";
                data += "IgnoreCase=True\n";
                data += "# Color filters\n";
                data += "F0Mode=1\n";
                data += "F0Text=\n";
                data += "F0Color=-29696\n";
                data += "F0Back=-1\n";
                data += "F1Mode=1\n";
                data += "F1Text=\n";
                data += "F1Color=-16776961\n";
                data += "F1Back=-1\n";
                data += "F2Mode=1\n";
                data += "F2Text=\n";
                data += "F2Color=-16728064\n";
                data += "F2Back=-1\n";
                data += "F3Mode=1\n";
                data += "F3Text=\n";
                data += "F3Color=-16751616\n";
                data += "F3Back=-1\n";
                data += "F4Mode=1\n";
                data += "F4Text=\n";
                data += "F4Color=-65281\n";
                data += "F4Back=-1\n";
                data += "F5Mode=1\n";
                data += "F5Text=\n";
                data += "F5Color=-8355840\n";
                data += "F5Back=-1\n";
                data += "F6Mode=1\n";
                data += "F6Text=\n";
                data += "F6Color=-4194112\n";
                data += "F6Back=-1\n";
                data += "F7Mode=1\n";
                data += "F7Text=\n";
                data += "F7Color=-16744448\n";
                data += "F7Back=-1\n";
                data += "F8Mode=1\n";
                data += "F8Text=\n";
                data += "F8Color=-16727872\n";
                data += "F8Back=-1\n";
                data += "F9Mode=1\n";
                data += "F9Text=\n";
                data += "F9Color=-24454\n";
                data += "F9Back=-1\n";
                data += "F10Mode=1\n";
                data += "F10Text=ERROR\n";
                data += "F10Color=-256\n";
                data += "F10Back=-65536\n";
                data += "F11Mode=1\n";
                data += "F11Text=\n";
                data += "F11Color=-16777216\n";
                data += "F11Back=-16711936\n";
                data += "\n";
                data += "[Connect]\n";
                data += "Type=TCPServer\n";
                data += "Serial=COM1\n";
                data += "Baudrate=115200\n";
                data += "DataBits=8\n";
                data += "Handshaking=None\n";
                data += "Parity=None\n";
                data += "StopBits=1\n";
                data += "InitialRTS=False\n";
                data += "InitialDTR=False\n";
                data += "TCPConnectAddress=localhost\n";
                data += "TCPConnectPort=5000\n";
                data += "TCPListenPort=5000\n";
                data += "\n";

                string filename = _directory + $"\\{name}.profile";
                File.WriteAllText(filename, data);
                return true;
            }
            catch { }
            return false;
        }
        public static bool SaveProfile(Profile profile, string filename)
        {
            // search string is special case
            profile.displayOptions.filter[SEARCH_INDEX].text = String.Empty;
            profile.displayOptions.filter[SEARCH_INDEX].mode = 1;

            try
            {
                AssemblyInfo info = new AssemblyInfo();
                string data = "# -----------------------------------------------------\n";
                data += $"# {info.Title} - v{info.AssemblyVersion} - {info.Copyright}\n";
                data += $"# Profile Exported: {DateTime.Now}\n";
                data += "# -----------------------------------------------------\n";
                data += $"\n";

                data += "[Profile]\n";
                data += $"Name={profile.name}\n";
                data += $"SendCR={profile.sendCR}\n";
                data += $"SendLF={profile.sendLF}\n";
                data += $"ClearCMD={profile.clearCMD}\n";
                data += $"OnTop={profile.stayontop}\n";
                data += $"TimeOutput={profile.displayOptions.timestampOutputLines}\n";

                data += $"ShowCR={profile.embellishments.ShowCR}\n";
                data += $"ShowLF={profile.embellishments.ShowLF}\n";
                data += $"ShowASCII={profile.embellishments.ShowASCII}\n";
                data += $"ShowHEX={profile.embellishments.ShowHEX}\n";
                data += $"TimeInput={profile.embellishments.ShowTimestamp}\n";

                if (!Directory.Exists(profile.logOptions.Directory))
                {
                    Directory.CreateDirectory(profile.logOptions.Directory);
                }

                data += $"LogDir={profile.logOptions.Directory}\n";
                data += $"LogPrefix={profile.logOptions.Prefix}\n";

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                string fontString = converter.ConvertToString(profile.displayOptions.outputFont);
                data += $"OutFont={fontString}\n";
                fontString = converter.ConvertToString(profile.displayOptions.inputFont);
                data += $"InFont={fontString}\n";

                data += $"# Input panel colours\n";
                data += $"InBackColor={profile.displayOptions.inputBackground.ToArgb()}\n";
                data += $"InTextColor={profile.displayOptions.inputDefaultForeground.ToArgb()}\n";

                data += $"# Output panel colours\n";
                data += $"OutBackColor={profile.displayOptions.outputBackground.ToArgb()}\n";
                data += $"IgnoreCase={profile.displayOptions.IgnoreCase}\n";

                data += $"# Color filters\n";
                for (int i = 0; i < profile.displayOptions.filter.Length; i++)
                {
                    if (profile.displayOptions.filter[i] != null)
                    {
                        data += $"F{i}Mode={profile.displayOptions.filter[i].mode}\n";
                        data += $"F{i}Text={profile.displayOptions.filter[i].text}\n";
                        data += $"F{i}Color={profile.displayOptions.filter[i].foreColor.ToArgb()}\n";
                        data += $"F{i}Back={profile.displayOptions.filter[i].backColor.ToArgb()}\n";
                    }
                }

                data += $"\n";

                data += "[Connect]\n";
                data += $"Type={profile.conOptions.Type}\n";
                data += $"Serial={profile.conOptions.SerialPort}\n";
                data += $"Baudrate={profile.conOptions.Baudrate}\n";
                data += $"DataBits={profile.conOptions.DataBits}\n";
                data += $"Handshaking={profile.conOptions.Handshaking}\n";
                data += $"Parity={profile.conOptions.Parity}\n";
                data += $"StopBits={profile.conOptions.StopBits}\n";
                data += $"InitialRTS={profile.conOptions.InitialRTS}\n";
                data += $"InitialDTR={profile.conOptions.InitialDTR}\n";
                data += $"TCPConnectAddress={profile.conOptions.TCPConnectAdress}\n";
                data += $"TCPConnectPort={profile.conOptions.TCPConnectPort}\n";
                data += $"TCPListenPort={profile.conOptions.TCPListenPort}\n";
                data += $"\n";

                int index = 0;
                foreach (Macro mac in profile.macros)
                {
                    if (mac != null && mac.title.Length > 0)
                    {
                        data += $"[{index}]\n";
                        data += $"Title={mac.title}\n";
                        data += $"ICD={mac.delayBetweenChars}\n";
                        data += $"ILD={mac.delayBetweenLines}\n";
                        data += $"RepeatON={mac.repeatEnabled}\n";
                        data += $"Delta={mac.resendEveryMs}\n";
                        data += $"Repeats={mac.stopAfterRepeats}\n";
                        data += $"Text={mac.macro}\n";
                        data += $"AddCR={mac.addCR}\n";
                        data += $"AddLF={mac.addLF}\n";
                        data += $"\n";
                    }
                    index++;
                }
                File.WriteAllText(filename, data);
                return true;
            }
            catch { }
            return false;
        }

        public static void SaveAsActiveProfile(string name)
        {
            string filename = _directory + $"\\Active";
            try
            {
                File.WriteAllText(filename, name);
            }
            catch { }
        }

        public static bool HasActiveProfile()
        {
            string filename = _directory + $"\\Active";
            return File.Exists(filename);
        }

        public static string GetActiveProfile()
        {
            string filename = _directory + $"\\Active";
            try
            {
                string name = File.ReadAllText(filename);
                return name;
            }
            catch { }
            return "Default";
        }

        public static Profile ReadProfile(string name)
        {
            try
            {
                Profile profile = new Profile();
                string filename = _directory + $"\\{name}.profile";

                string data = File.ReadAllText(filename);
                string[] lines = data.Split(new char[] { '\r', '\n' });
                FSection section = FSection.None;
                int mid = -1;

                profile.displayOptions.IgnoreCase = true;   // unless overwritten

                foreach (string line in lines)
                {
                    if (line.Equals("[Profile]"))
                    {
                        section = FSection.Profile;
                    }
                    else if (line.Equals("[Connect]"))
                    {
                        section = FSection.Connections;
                    }
                    else if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        section = FSection.Macros;
                        mid = Utils.Int(line.Substring(1, line.Length - 2));
                        if (mid < 0 || mid >= profile.macros.Length)
                        {
                            throw new Exception();
                        }

                        if (profile.macros[mid] == null)
                        {
                            profile.macros[mid] = new Macro();
                        }
                    }
                    else if (section == FSection.Profile)
                    {
                        if (line.StartsWith("Name="))
                        {
                            profile.name = line.Substring(5);
                        }
                        else if (line.StartsWith("SendCR="))
                        {
                            profile.sendCR = line.Contains("True");
                        }
                        else if (line.StartsWith("SendLF="))
                        {
                            profile.sendLF = line.Contains("True");
                        }
                        else if (line.StartsWith("ClearCMD="))
                        {
                            profile.clearCMD = line.Contains("True");
                        }
                        else if (line.StartsWith("OnTop="))
                        {
                            profile.stayontop = line.Contains("True");
                        }
                        else if (line.StartsWith("ShowCR="))
                        {
                            profile.embellishments.ShowCR = line.Contains("True");
                        }
                        else if (line.StartsWith("ShowLF="))
                        {
                            profile.embellishments.ShowLF = line.Contains("True");
                        }
                        else if (line.StartsWith("ShowASCII="))
                        {
                            profile.embellishments.ShowASCII = line.Contains("True");
                        }
                        else if (line.StartsWith("ShowHEX="))
                        {
                            profile.embellishments.ShowHEX = line.Contains("True");
                        }
                        else if (line.StartsWith("TimeInput="))
                        {
                            profile.embellishments.ShowTimestamp = line.Contains("True");
                        }
                        else if (line.StartsWith("OutFont="))
                        {
                            if (!line.EndsWith("(none)"))
                            {
                                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                                profile.displayOptions.outputFont = (Font)converter.ConvertFromString(line.Substring(8));
                            }
                        }

                        else if (line.StartsWith("InFont="))
                        {
                            if (!line.EndsWith("(none)"))
                            {
                                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                                profile.displayOptions.inputFont = (Font)converter.ConvertFromString(line.Substring(7));
                            }
                        }

                        else if (line.StartsWith("TimeOutput="))
                        {
                            profile.displayOptions.timestampOutputLines = line.Contains("True");
                        }
                        else if (line.StartsWith("InBackColor="))
                        {
                            profile.displayOptions.inputBackground = Color.FromArgb(Utils.Int(line.Substring(12)));
                        }
                        else if (line.StartsWith("InTextColor="))
                        {
                            profile.displayOptions.inputDefaultForeground = Color.FromArgb(Utils.Int(line.Substring(12)));
                        }
                        else if (line.StartsWith("OutBackColor="))
                        {
                            profile.displayOptions.outputBackground = Color.FromArgb(Utils.Int(line.Substring(13)));
                        }
                        else if (line.StartsWith("IgnoreCase="))
                        {
                            profile.displayOptions.IgnoreCase = line.Contains("True");
                        }
                        else if (line.StartsWith("LogDir="))
                        {
                            profile.logOptions.Directory = line.Substring(7);
                        }
                        else if (line.StartsWith("LogPrefix="))
                        {
                            profile.logOptions.Prefix = line.Substring(10);
                        }
                        else
                        {
                            for (int i = 0; i < profile.displayOptions.filter.Length; i++)
                            {
                                string key = $"F{i}Mode=";
                                if (line.StartsWith(key))
                                {
                                    profile.displayOptions.filter[i].mode = Utils.Int(line.Substring(key.Length));
                                }

                                key = $"F{i}Text=";
                                if (line.StartsWith(key))
                                {
                                    profile.displayOptions.filter[i].text = line.Substring(key.Length);
                                }

                                key = $"F{i}Color=";
                                if (line.StartsWith(key))
                                {
                                    profile.displayOptions.filter[i].foreColor = Color.FromArgb(Utils.Int(line.Substring(key.Length)));
                                }

                                key = $"F{i}Back=";
                                if (line.StartsWith(key))
                                {
                                    profile.displayOptions.filter[i].backColor = Color.FromArgb(Utils.Int(line.Substring(key.Length)));
                                }
                            }
                        }
                    }
                    else if (section == FSection.Connections)
                    {
                        if (line.Equals("Type=Serial"))
                        {
                            profile.conOptions.Type = ConOptions.ConType.Serial;
                        }
                        else if (line.Equals("Type=TCPClient"))
                        {
                            profile.conOptions.Type = ConOptions.ConType.TCPClient;
                        }
                        else if (line.Equals("Type=TCPServer"))
                        {
                            profile.conOptions.Type = ConOptions.ConType.TCPServer;
                        }
                        else if (line.StartsWith("Serial="))
                        {
                            profile.conOptions.SerialPort = line.Substring(7);
                        }
                        else if (line.StartsWith("Baudrate="))
                        {
                            profile.conOptions.Baudrate = line.Substring(9);
                        }
                        else if (line.StartsWith("DataBits="))
                        {
                            profile.conOptions.DataBits = line.Substring(9);
                        }
                        else if (line.StartsWith("Handshaking="))
                        {
                            profile.conOptions.Handshaking = line.Substring(12);
                        }
                        else if (line.StartsWith("Parity="))
                        {
                            profile.conOptions.Parity = line.Substring(7);
                        }
                        else if (line.StartsWith("StopBits="))
                        {
                            profile.conOptions.StopBits = line.Substring(9);
                        }
                        else if (line.StartsWith("InitialRTS="))
                        {
                            profile.conOptions.InitialRTS = line.Contains("True");
                        }
                        else if (line.StartsWith("InitialDTR="))
                        {
                            profile.conOptions.InitialDTR = line.Contains("True");
                        }
                        else if (line.StartsWith("TCPConnectAddress="))
                        {
                            profile.conOptions.TCPConnectAdress = line.Substring(18);
                        }
                        else if (line.StartsWith("TCPConnectPort="))
                        {
                            profile.conOptions.TCPConnectPort = line.Substring(15);
                        }
                        else if (line.StartsWith("TCPListenPort="))
                        {
                            profile.conOptions.TCPListenPort = line.Substring(14);
                        }
                    }
                    else if (section == FSection.Macros)
                    {
                        if (mid < 0 || mid >= profile.macros.Length)
                        {
                            throw new Exception();
                        }

                        Macro mac = profile.macros[mid];

                        if (line.StartsWith("Title="))
                        {
                            mac.title = line.Substring(6);
                        }
                        else if (line.StartsWith("ICD="))
                        {
                            mac.delayBetweenChars = Utils.Int(line.Substring(4));
                        }
                        else if (line.StartsWith("ILD="))
                        {
                            mac.delayBetweenLines = Utils.Int(line.Substring(4));
                        }
                        else if (line.StartsWith("RepeatON="))
                        {
                            mac.repeatEnabled = line.Contains("True");
                        }
                        else if (line.StartsWith("Delta="))
                        {
                            mac.resendEveryMs = Utils.Int(line.Substring(6));
                        }
                        else if (line.StartsWith("Repeats="))
                        {
                            mac.stopAfterRepeats = Utils.Int(line.Substring(8));
                        }
                        else if (line.StartsWith("Text="))
                        {
                            mac.macro = line.Substring(5);
                        }
                        else if (line.StartsWith("AddCR="))
                        {
                            mac.addCR = line.Contains("True");
                        }
                        else if (line.StartsWith("AddLF="))
                        {
                            mac.addLF = line.Contains("True");
                        }
                    }
                }
                profile.name = name;
                return profile;
            }
            catch { }
            return new Profile();
        }
    }
}