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
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;

using static System.Net.Mime.MediaTypeNames;

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
        public bool RestartServer = false;

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
        public int MaxLogSize = 0;   // 0 = infinite
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
        public int delayBetweenLinesMs = 200;
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
            return (Macro)this.MemberwiseClone();
        }
    }

    public class ColorFilter
    {
        public int mode = 0;
        public string text = string.Empty;
        public Color foreColor = Color.Black;
        public Color backColor = Color.White;
        public string macro = string.Empty;
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

        public bool ShowOutputTimestamp = true;

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
            DisplayOptions opt = new DisplayOptions();
            opt.inputFont = inputFont;
            opt.outputFont = outputFont;
            opt.inputDefaultForeground = inputDefaultForeground;
            opt.inputBackground = inputBackground;
            opt.IgnoreCase = IgnoreCase;
            opt.outputBackground = outputBackground;
            opt.ShowOutputTimestamp = ShowOutputTimestamp;

            for (int f = 0; f < filter.Length; f++)
            {
                if (filter[f] != null)
                {
                    opt.filter[f] = new ColorFilter();
                    opt.filter[f].mode = filter[f].mode;
                    opt.filter[f].text = filter[f].text;
                    opt.filter[f].foreColor = filter[f].foreColor;
                    opt.filter[f].backColor = filter[f].backColor;
                    opt.filter[f].macro = filter[f].macro;
                }
                else
                    opt.filter[f] = null;
            }
            return opt;
        }
    }

    public class Embellishments
    {
        public bool ShowCR;
        public bool ShowLF;
        public bool ShowInputTimestamp;
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
        public bool sendAsIType = false;
        public bool stayontop = false;

        public Embellishments embellishments = new Embellishments();
        public ConOptions conOptions = new ConOptions();
        public LoggingOptions logOptions = new LoggingOptions();
        public DisplayOptions displayOptions = new DisplayOptions();
        public Macro[] macros = new Macro[48 * 4];
        public string[] titles = new string[12 * 4];

        public Profile()
        {
            for (int m = 0; m < macros.Length; m++)
                macros[m] = new Macro();
        }
    }

    public class Utils
    {
        private static Stopwatch stopwatchT = new Stopwatch();
        private static int millisecondsSinceMidnight = 0;

        private static int GetTimeProgramStarted()
        {
            // Get current DateTime (in local time zone, consistent with typical "now")
            DateTime now = DateTime.Now;

            // Compute total milliseconds since midnight
            int msSinceMidnight = now.Hour * 3600000 + now.Minute * 60000 + now.Second * 1000 + now.Millisecond;

            return msSinceMidnight;
        }

        private static string GetTime(int timeProgramStarted, double elapsedTime)
        {
            // Total milliseconds since midnight
            double totalMs = timeProgramStarted + elapsedTime;

            // Total seconds since midnight (with full precision)
            double totalSec = totalMs / 1000.0;

            // Extract hours
            int hours = (int)(totalSec / 3600.0);

            // Remaining seconds after full hours
            double remSec = totalSec % 3600.0;

            // Extract minutes
            int minutes = (int)(remSec / 60.0);

            // Remaining seconds after full minutes
            double remSecAfterMin = remSec % 60.0;

            // Extract whole seconds (0-59)
            int seconds = (int)remSecAfterMin;

            // Get the fractional part of the seconds (0.0 to 0.999...)
            double frac = remSecAfterMin - seconds;

            // Calculate milliseconds from fractional part (multiply by 1000)
            double msValue = frac * 1000.0;

            // Whole milliseconds (0-999)
            int ms = (int)msValue;

            // Remaining fraction for microseconds
            double subMs = msValue - ms;

            // Calculate microseconds (round to nearest)
            int us = (int)Math.Round(subMs * 1000.0);

            // Format hours, minutes, seconds, and ms with leading zeros
            // uuu similarly
            string timeStr = $"{hours:D2}:{minutes:D2}:{seconds:D2}.{ms:D3} {us:D3}: ";

            return timeStr;
        }

        public static void StartStopwatch()
        {
            millisecondsSinceMidnight = GetTimeProgramStarted();
            stopwatchT.Start();
        }

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
            if (millisecondsSinceMidnight >= 86400000)
            {
                //midnight roll-over
                stopwatchT.Stop();
                StartStopwatch();
            }

            return GetTime(millisecondsSinceMidnight, stopwatchT.Elapsed.TotalMilliseconds);
        }
        public static bool IsNumeric(char ch)
        {
            if (ch < '0' || ch > '9')
                return false;
            return true;
        }

        //  012345678901234567
        // "HH:mm:ss.mmm mmm: "
        public static bool HasTimestamp(string str)
        {
            if (str.Length < 18)
                return false;
            if (!IsNumeric(str[0]) || !IsNumeric(str[1]) || !IsNumeric(str[3]) || !IsNumeric(str[4]) || !IsNumeric(str[6]) || !IsNumeric(str[7]) || !IsNumeric(str[9]) || !IsNumeric(str[10]) || !IsNumeric(str[11]) || !IsNumeric(str[13]) || !IsNumeric(str[14]) || !IsNumeric(str[15]))
                return false;
            if (str[2] != (char)':' || str[5] != (char)':' || str[8] != (char)'.' || str[12] != (char)' ' || str[16] != (char)':')
                return false;
            return true;
        }

        //           123456789012345678
        // Assuming "HH:mm:ss.mmm mmm: "
        public static int TimestampLength()
        {
            return 18;
        }
    }
}