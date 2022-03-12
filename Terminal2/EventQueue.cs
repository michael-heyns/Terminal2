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
            _lengthOfStringObjects += item.ToString().Length;
            base.Enqueue(item);
            _main.Invoke(_handler);
        }

        protected virtual void OnItemAdded(EventArgs e)
        {
            _main.Invoke(_handler);
        }
    }
}