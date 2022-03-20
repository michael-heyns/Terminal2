using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

using static System.Collections.Specialized.BitVector32;

namespace Terminal
{
    internal class Database
    {
        private enum FSection { None, Profile, Connections, Macros };
        private static readonly string _directory = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Terminal2";

        public static void Initialise()
        {
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);
            Directory.SetCurrentDirectory(_directory);
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
                            return true;
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
                else if (File.Exists(fullpath))
                {
                    File.Copy(fullpath, dstf);
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
                    File.Delete(fname);
            }
            catch { }
        }

        public static bool SaveProfile(Profile profile)
        {
            string filename = _directory + $"\\{profile.name}.profile";
            return SaveProfile(profile, filename);
        }
        public static bool SaveProfile(Profile profile, string filename)
        {
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
                data += $"ShowCR={profile.showCurlyCR}\n";
                data += $"ShowLF={profile.showCurlyLF}\n";
                data += $"ShowASCII={profile.ascii}\n";
                data += $"TranslateCR={profile.translateCR}\n";
                data += $"TranslateLF={profile.translateLF}\n";
                data += $"SendCR={profile.sendCR}\n";
                data += $"SendLF={profile.sendLF}\n";
                data += $"ClearCMD={profile.clearCMD}\n";
                data += $"OnTop={profile.stayontop}\n";
                data += $"TimeInput={profile.timestampInput}\n";
                data += $"TimeOutput={profile.displayOptions.timestampOutputLines}\n";
                data += $"MaxBufSizeMB={profile.displayOptions.maxBufferSizeMB}\n";
                data += $"CutPercent={profile.displayOptions.cutPercent}\n";
                data += $"FreezeKB={profile.displayOptions.freezeSizeKB}\n";

                if (!Directory.Exists(profile.logOptions.Directory))
                    Directory.CreateDirectory(profile.logOptions.Directory);
                data += $"LogDir={profile.logOptions.Directory}\n";
                data += $"LogPrefix={profile.logOptions.Prefix}\n";

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                string fontString = converter.ConvertToString(profile.displayOptions.outputFont);
                data += $"OutFont={fontString}\n";
                fontString = converter.ConvertToString(profile.displayOptions.inputFont);
                data += $"InFont={fontString}\n";

                data += $"# Input panel colours\n";
                data += $"InBackColor={profile.displayOptions.inputBackground.ToArgb()}\n";
                data += $"InTextColor={profile.displayOptions.inputText.ToArgb()}\n";

                data += $"# Output panel colours\n";
                data += $"OutBackColor={profile.displayOptions.outputBackground.ToArgb()}\n";

                data += $"# Color filters\n";
                data += $"ColorFilters={profile.displayOptions.colorFiltersEnabled}\n";
                for (int i = 0; i < profile.displayOptions.lines.Length; i++)
                {
                    if (profile.displayOptions.lines[i] != null)
                    {
                        data += $"F{i}Mode={profile.displayOptions.lines[i].mode}\n";
                        data += $"F{i}Text={profile.displayOptions.lines[i].text}\n";
                        data += $"F{i}Color={profile.displayOptions.lines[i].color.ToArgb()}\n";
                        data += $"F{i}Freeze={profile.displayOptions.lines[i].freeze}\n";
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

                foreach (string line in lines)
                {
                    if (line.Equals("[Profile]"))
                        section = FSection.Profile;
                    else if (line.Equals("[Connect]"))
                        section = FSection.Connections;
                    else if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        section = FSection.Macros;
                        mid = Utils.Int(line.Substring(1, line.Length - 2));
                        if (mid < 0 || mid >= profile.macros.Length)
                            throw new Exception();

                        if (profile.macros[mid] == null)
                            profile.macros[mid] = new Macro();
                    }
                    else if (section == FSection.Profile)
                    {
                        if (line.StartsWith("Name="))
                            profile.name = line.Substring(5);
                        else if (line.StartsWith("ShowCR="))
                            profile.showCurlyCR = line.Contains("True");
                        else if (line.StartsWith("ShowLF="))
                            profile.showCurlyLF = line.Contains("True");
                        else if (line.StartsWith("ShowASCII="))
                            profile.ascii = line.Contains("True");
                        else if (line.StartsWith("TranslateCR="))
                            profile.translateCR = Utils.Int(line.Substring(12));
                        else if (line.StartsWith("TranslateLF="))
                            profile.translateLF = Utils.Int(line.Substring(12));
                        else if (line.StartsWith("SendCR="))
                            profile.sendCR = line.Contains("True");
                        else if (line.StartsWith("SendLF="))
                            profile.sendLF = line.Contains("True");
                        else if (line.StartsWith("ClearCMD="))
                            profile.clearCMD = line.Contains("True");
                        else if (line.StartsWith("OnTop="))
                            profile.stayontop = line.Contains("True");
                        else if (line.StartsWith("TimeInput="))
                            profile.timestampInput = line.Contains("True");

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

                        else if (line.StartsWith("MaxBufSizeMB="))
                            profile.displayOptions.maxBufferSizeMB = Utils.Int(line.Substring(13));
                        else if (line.StartsWith("CutPercent="))
                            profile.displayOptions.cutPercent = Utils.Int(line.Substring(11));
                        else if (line.StartsWith("FreezeKB="))
                            profile.displayOptions.freezeSizeKB = Utils.Int(line.Substring(9));

                        else if (line.StartsWith("TimeOutput="))
                            profile.displayOptions.timestampOutputLines = line.Contains("True");

                        else if (line.StartsWith("InBackColor="))
                            profile.displayOptions.inputBackground = Color.FromArgb(Utils.Int(line.Substring(12)));
                        else if (line.StartsWith("InTextColor="))
                            profile.displayOptions.inputText = Color.FromArgb(Utils.Int(line.Substring(12)));

                        else if (line.StartsWith("OutBackColor="))
                            profile.displayOptions.outputBackground = Color.FromArgb(Utils.Int(line.Substring(13)));

                        else if (line.StartsWith("ColorFilters="))
                            profile.displayOptions.colorFiltersEnabled = line.Contains("True");
                        else if (line.StartsWith("LogDir="))
                            profile.logOptions.Directory = line.Substring(7);
                        else if (line.StartsWith("LogPrefix="))
                            profile.logOptions.Prefix = line.Substring(10);
                        else
                        {
                            for (int i = 0; i < profile.displayOptions.lines.Length; i++)
                            {
                                string key = $"F{i}Mode=";
                                if (line.StartsWith(key))
                                    profile.displayOptions.lines[i].mode = Utils.Int(line.Substring(key.Length));
                                key = $"F{i}Text=";
                                if (line.StartsWith(key))
                                    profile.displayOptions.lines[i].text = line.Substring(key.Length);
                                key = $"F{i}Color=";
                                if (line.StartsWith(key))
                                    profile.displayOptions.lines[i].color = Color.FromArgb(Utils.Int(line.Substring(key.Length)));
                                key = $"F{i}Freeze=";
                                if (line.StartsWith(key))
                                    profile.displayOptions.lines[i].freeze = line.Contains("True");
                            }
                        }
                    }
                    else if (section == FSection.Connections)
                    {
                        if (line.Equals("Type=Serial"))
                            profile.conOptions.Type = ConOptions.ConType.Serial;
                        else if (line.Equals("Type=TCPClient"))
                            profile.conOptions.Type = ConOptions.ConType.TCPClient;
                        else if (line.Equals("Type=TCPServer"))
                            profile.conOptions.Type = ConOptions.ConType.TCPServer;
                        else if (line.StartsWith("Serial="))
                            profile.conOptions.SerialPort = line.Substring(7);
                        else if (line.StartsWith("Baudrate="))
                            profile.conOptions.Baudrate = line.Substring(9);
                        else if (line.StartsWith("DataBits="))
                            profile.conOptions.DataBits = line.Substring(9);
                        else if (line.StartsWith("Handshaking="))
                            profile.conOptions.Handshaking = line.Substring(12);
                        else if (line.StartsWith("Parity="))
                            profile.conOptions.Parity = line.Substring(7);
                        else if (line.StartsWith("StopBits="))
                            profile.conOptions.StopBits = line.Substring(9);
                        else if (line.StartsWith("TCPConnectAddress="))
                            profile.conOptions.TCPConnectAdress = line.Substring(18);
                        else if (line.StartsWith("TCPConnectPort="))
                            profile.conOptions.TCPConnectPort = line.Substring(15);
                        else if (line.StartsWith("TCPListenPort="))
                            profile.conOptions.TCPListenPort = line.Substring(14);

                    }
                    else if (section == FSection.Macros)
                    {
                        if (mid < 0 || mid >= profile.macros.Length)
                            throw new Exception();

                        Macro mac = profile.macros[mid];

                        if (line.StartsWith("Title="))
                            mac.title = line.Substring(6);
                        else if (line.StartsWith("ICD="))
                            mac.delayBetweenChars = Utils.Int(line.Substring(4));
                        else if (line.StartsWith("ILD="))
                            mac.delayBetweenLines = Utils.Int(line.Substring(4));
                        else if (line.StartsWith("RepeatON="))
                            mac.repeatEnabled = line.Contains("True");
                        else if (line.StartsWith("Delta="))
                            mac.resendEveryMs = Utils.Int(line.Substring(6));
                        else if (line.StartsWith("Repeats="))
                            mac.stopAfterRepeats = Utils.Int(line.Substring(8));
                        else if (line.StartsWith("Text="))
                            mac.macro = line.Substring(5);
                        else if (line.StartsWith("AddCR="))
                            mac.addCR = line.Contains("True");
                        else if (line.StartsWith("AddLF="))
                            mac.addLF = line.Contains("True");
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