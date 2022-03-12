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
        public bool run = false;
        public int runNextLine = 0;
        public int runRepeats = 0;
        public long runTimeout = 0;
        public string[] runLines = null;

        // helpers
        public int uiColumn;
        public int uiRow;

        public Macro Clone()
        {
            return (Macro)MemberwiseClone();
        }
    }

    public class ColorLines
    {
        public int mode = 0;
        public string text = string.Empty;
        public Color color = Color.Black;
        public bool freeze = false;
        public ColorLines Clone()
        {
            return (ColorLines)MemberwiseClone();
        }
    }

    public class DisplayOptions
    {
        public bool colorFiltersEnabled = false;
        public ColorLines[] lines = new ColorLines[12];

        public Font inputFont;
        public Color inputText = Color.Black;
        public Color inputBackground = Color.White;

        public Font outputFont;
        public Color outputBackground = Color.Gainsboro;

        public bool timestampOutputLines = true;

        public int maxBufferSizeMB = 10;
        public int cutPercent = 10;
        public int freezeSizeKB = 10;

        public DisplayOptions()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            inputFont = (Font)converter.ConvertFromString(Utils.DefaultInputFont);
            outputFont = (Font)converter.ConvertFromString(Utils.DefaultOutputFont);
            for (int i = 0; i < lines.Length; i++)
                lines[i] = new ColorLines();
        }

        public DisplayOptions Clone()
        {
            DisplayOptions opt = (DisplayOptions)this.MemberwiseClone();
            for (int dl = 0; dl < lines.Length; dl++)
            {
                if (lines[dl] != null)
                    opt.lines[dl] = lines[dl].Clone();
                else
                    opt.lines[dl] = null;
            }
            return opt;
        }
    }

    public class Profile
    {
        public bool active = false;

        public string name = "Default";
        public bool showCurlyCR = false;
        public bool showCurlyLF = false;
        public bool ascii = true;
        public bool hex = false;
        public bool endOnCR = true;
        public bool sendCR = true;
        public bool sendLF = false;
        public bool stayontop = false;
        public bool timestampInput = true;

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
            return $"{DateTime.Now:HH:mm:ss.ff}: ";
        }
    }
}