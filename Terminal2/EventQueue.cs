/* 
 * Terminal2
 *
 * Copyright © 2022-23-23 Michael Heyns
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
using System.Collections;

using static Terminal.FrmMain;

namespace Terminal
{
    internal class EventQueue : Queue
    {
        private readonly FrmMain _main;
        private readonly EnqueuedDelegate _handler;
        private int _lengthOfStringObjects = 0;
        public int LengthOfStringObjects
        {
            get { return _lengthOfStringObjects; }
        }
        public EventQueue(FrmMain main, EnqueuedDelegate handler)
        {
            _main = main;
            _handler = handler;
        }

        public delegate void SetAddedEventHandler(object sender, EventArgs e);
        public override int Count => base.Count;

        public override object Dequeue()
        {
            object obj = base.Dequeue();
            _lengthOfStringObjects -= obj.ToString().Length;
            return obj;
        }

        public override void Enqueue(object item)
        {
            try
            {
                _lengthOfStringObjects += item.ToString().Length;
                base.Enqueue(item);
                _main.Invoke(_handler);
            }
            catch { }
        }

        protected virtual void OnItemAdded(EventArgs e)
        {
            _main.Invoke(_handler);
        }
    }
}