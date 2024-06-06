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
using System.Diagnostics;
using System.IO;
using System.Text;

using static System.Net.WebRequestMethods;

namespace Terminal
{
    public class STXETX
    {
        private char STX = (char)0x02;
        private char ETX = (char)0x03;
        private char DLE = (char)0x10;

        private bool IsHex(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;
            else if (ch >= 'A' && ch <= 'F')
                return true;
            else if (ch >= 'a' && ch <= 'h')
                return true;
            return true;
        }
        private int GetHex(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return ch - '0';
            else if (ch >= 'A' && ch <= 'F')
                return (ch - 'A') + 10;
            else if (ch >= 'a' && ch <= 'h')
                return (ch - 'a') + 10;
            return 0;
        }

        private byte CalculateLrc(string msg, int len)
        {
            byte lrc = 0;
            for (int i = 0; i < len; i++)
            {
                if (msg[i] == '$' && IsHex(msg[i + 1]) && IsHex(msg[i + 2]))
                {
                    int ch = (GetHex(msg[i + 1]) << 4) | GetHex(msg[i + 2]);
                    lrc ^= (byte)ch;
                    i += 2;
                }
                else
                {
                    lrc ^= (byte)msg[i];
                }
            }
            return lrc;
        }

        private string InsertDLE(string msg)
        {
            int i = 0;
            int len = msg.Length;
            while (i < len)
            {
                int ch = msg[i];
                if (msg[i] == '$' && IsHex(msg[i + 1]) && IsHex(msg[i + 2]))
                {
                    ch = (GetHex(msg[i + 1]) << 4) | GetHex(msg[i + 2]);
                    if (ch == STX || ch == ETX || ch == DLE)
                    {
                        msg = msg.Insert(i, "$10");
                        len = msg.Length;
                        i += 5; // onto last character
                    }
                }
                i++; // next character
            }
            return msg;
        }

        // NON-$ string
        private string RemoveDLE(string msg)
        {
            int i = 0;
            int len = msg.Length;
            while (i < len)
            {
                char c = msg[i];
                if (c == DLE)
                {
                    msg = msg.Remove(i, 1);
                    len = msg.Length;
                }
                i++;
            }
            return msg;
        }

        public string Encode(string msg)
        {
            msg = InsertDLE(msg);
            msg = msg + "$03"; // add ETX
            byte lrc = CalculateLrc(msg, msg.Length);
            return "$02" + msg + "$" + lrc.ToString("X2");
        }

        public bool Verify(string msg)
        {
            if (msg == null || msg == string.Empty || msg.Length < 3) // Should be at least 3 bytes long (STX EXT LRC)
                return false;

            if (msg[0] != STX) // First character must be STX
                return false;

            if (msg[msg.Length - 2] != ETX) // The second last character must be ETX
                return false;

            // The last character is LRC. Compare it with the calculated value.
            char lrc = (char)CalculateLrc(msg.Substring(1), msg.Length - 2);
            return (msg[msg.Length - 1] == lrc);
        }

        // NON-$ string
        public string Decode(string msg)
        {
            if (!Verify(msg))
                return string.Empty;

            msg = msg.Substring(1, msg.Length - 3);    // without STX, ETX and LRC
            msg = RemoveDLE(msg);
            return msg;
        }
    }
}

