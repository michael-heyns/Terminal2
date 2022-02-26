using System;
using System.Collections;

using static Terminal.FrmMain;

namespace Terminal
{
    internal class EventQueue : Queue
    {
        private readonly FrmMain _main;
        private readonly EnqueuedDelegate _handler;
        public EventQueue(FrmMain main, EnqueuedDelegate handler)
        {
            _main = main;
            _handler = handler;
        }

        public delegate void SetAddedEventHandler(object sender, EventArgs e);
        public override int Count => base.Count;

        public override object Dequeue()
        {
            return base.Dequeue();
        }

        public override void Enqueue(object item)
        {
            base.Enqueue(item);
            _main.Invoke(_handler);
        }

        protected virtual void OnItemAdded(EventArgs e)
        {
            _main.Invoke(_handler);
        }
    }
}