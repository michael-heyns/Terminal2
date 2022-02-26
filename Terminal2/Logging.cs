/*
 * Logging.cs
 *
 *  Created on: 12/01/2022
 *      Author: Michael.Heyns
 *
 * Last Modified:
*/

using System;
using System.IO;

namespace Terminal
{
    internal class Logging
    {
        private readonly string log_file = "log.txt";

        public Logging(string logFileName)
        {
            this.log_file = logFileName;

            using (StreamWriter w = File.AppendText(this.log_file))
            {
                w.WriteLine($"==========================================================");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine($"==========================================================");
            }

            Log("Program start");
        }

        ~Logging()
        {
            Log("Program terminate");
        }

        public void Log(string text)
        {
            using (StreamWriter w = File.AppendText(log_file))
                w.WriteLine($"{DateTime.Now:HH:mm:ss.ff} : {text}");
        }
    }
}