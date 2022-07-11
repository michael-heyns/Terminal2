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

namespace Terminal
{
    public class ConOptions
    {
        public bool Modified;
        public enum ConType
        { Serial, TCPServer, TCPClient };
        public ConType Type;

        public string SerialPort = "COM1";
        public string Baudrate = "115200";
        public string DataBits = "8";
        public string Handshaking = "None";
        public string Parity = "None";
        public string StopBits = "1";
        public string TCPListenPort = "5000";
        public string TCPConnectAdress = "localhost";
        public string TCPConnectPort = "5000";
        public bool InitialDTR = false;
        public bool InitialRTS = false;

        public ConOptions Clone()
        {
            return (ConOptions)MemberwiseClone();
        }
    }

    public class LoggingOptions
    {
        public bool Modified;
        public string Directory = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Logs";
        public string Prefix = "Default_";
        public bool ShowControl = true;
        public LoggingOptions Clone()
        {
            return (LoggingOptions)MemberwiseClone();
        }
    }

    public class Macro
    {
        public int index = 0;

        // attributes
        public string title = string.Empty;
        public int delayBetweenChars = 0;
        public int delayBetweenLines = 0;
        public bool addCR = true;
        public bool addLF = false;
        public string macro = string.Empty;

        // repeats
        public bool repeatEnabled = false;
        public int resendEveryMs = 0;
        public int stopAfterRepeats = 0;

        // run control
        public string[] runLines = null;

        // helpers
        public int uiColumn;
        public int uiRow;

        public Macro Clone()
        {
            return (Macro)MemberwiseClone();
        }
    }

    public class ColorFilter
    {
        public int mode = 0;
        public string text = string.Empty;
        public Color foreColor = Color.Black;
        public Color backColor = Color.White;
        public ColorFilter Clone()
        {
            return (ColorFilter)MemberwiseClone();
        }
    }

    public class DisplayOptions
    {
        public ColorFilter[] filter = new ColorFilter[12];
        public bool IgnoreCase = true;

        public Font inputFont;
        public Color inputDefaultForeground = Color.Black;
        public Color inputBackground = Color.White;

        public Font outputFont;
        public Color outputBackground = Color.Gainsboro;

        public bool timestampOutputLines = true;

        public DisplayOptions()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            inputFont = (Font)converter.ConvertFromString(Utils.DefaultInputFont);
            outputFont = (Font)converter.ConvertFromString(Utils.DefaultOutputFont);
            for (int i = 0; i < filter.Length; i++)
                filter[i] = new ColorFilter();
        }

        public DisplayOptions Clone()
        {
            DisplayOptions opt = (DisplayOptions)this.MemberwiseClone();
            for (int dl = 0; dl < filter.Length; dl++)
            {
                if (filter[dl] != null)
                    opt.filter[dl] = filter[dl].Clone();
                else
                    opt.filter[dl] = null;
            }
            return opt;
        }
    }

    public class Embellishments
    {
        public bool ShowCR;
        public bool ShowLF;
        public bool ShowTimestamp;
        public bool ShowASCII;
        public bool ShowHEX;

        public Embellishments Clone()
        {
            Embellishments emb = (Embellishments)this.MemberwiseClone();
            return emb;
        }
    }

    public class Profile
    {
        public bool active = false;

        public string name = "Default";
        public bool sendCR = true;
        public bool sendLF = false;
        public bool clearCMD = false;
        public bool stayontop = false;

        public Embellishments embellishments = new Embellishments();
        public ConOptions conOptions = new ConOptions();
        public LoggingOptions logOptions = new LoggingOptions();
        public DisplayOptions displayOptions = new DisplayOptions();
        public Macro[] macros = new Macro[48];

        public Profile()
        {
            for (int m = 0; m < macros.Length; m++)
                macros[m] = new Macro();
        }

        public Profile Clone(string name)
        {
            Profile s = (Profile)this.MemberwiseClone();
            s.name = name;
            s.embellishments = embellishments.Clone();
            s.conOptions = conOptions.Clone();
            s.logOptions = logOptions.Clone();
            s.displayOptions = displayOptions.Clone();
            s.conOptions.Modified = false;
            s.logOptions.Modified = false;
            for (int m = 0; m < macros.Length; m++)
            {
                if (macros[m] != null)
                    s.macros[m] = macros[m].Clone();
                else
                    s.macros[m] = null;
            }
            return s;
        }
    }

    public class Utils
    {

        public const string DefaultInputFont = "Courier New, 11.25pt";
        public const string DefaultOutputFont = "Courier New, 8.25pt";
        public static int Int(string str)
        {
            try
            {
                Int32.TryParse(str, out int ivalue);
                return ivalue;
            }
            catch { }
            return 0;
        }

        public static string TimestampForFilename()
        {
            return $"{DateTime.Now:yyMMdd_HHmmss}";
        }

        public static string Timestamp()
        {
            return $"{DateTime.Now:HH:mm:ss.fff}: ";
        }
        public static bool IsNumeric(char ch)
        {
            if (ch < '0' || ch > '9')
                return false;
            return true;
        }

        // "HH:mm:ss.fff: "
        public static bool HasTimestamp(string str)
        {
            if (str.Length < 14)
                return false;
            if (!IsNumeric(str[0]) || !IsNumeric(str[1]) || !IsNumeric(str[3]) || !IsNumeric(str[4]) || !IsNumeric(str[6]) || !IsNumeric(str[7]) || !IsNumeric(str[9]) || !IsNumeric(str[10]) || !IsNumeric(str[11]))
                return false;
            if (str[2] != (char)':' || str[5] != (char)':' || str[8] != (char)'.' || str[12] != (char)':')
                return false;
            return true;
        }

        public static int TimestampLength()
        {
            return 14;
        }
    }
}