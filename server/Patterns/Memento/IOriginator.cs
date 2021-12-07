using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomermanServer.Patterns.Memento
{
    public interface IOriginator<T>
    {
        IMemento GetState();
        void Restore(T data);   
    }
}