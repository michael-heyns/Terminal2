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