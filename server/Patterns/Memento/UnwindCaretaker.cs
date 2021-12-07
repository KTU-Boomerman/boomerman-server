using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Memento
{
    public class UnwindCaretaker
    {
        private List<IMemento> _history;

        public UnwindCaretaker()
        {
            _history = new List<IMemento>();
        }

        public void Save(IMemento memento)
        {
            _history.Add(memento);
            
            if (_history.Count > 3)
                _history.RemoveAt(0);
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                _history.First().Restore();
            }
        }
    }
}