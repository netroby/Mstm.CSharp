using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mstm.Common.Thread
{
    public class SingletonWrap<T>
       where T : class, new()
    {
        private static object @lock = new object();

        private static T _instance;

        private SingletonWrap() { }

        public T GetSingleton()
        {
            if (_instance != null) { return _instance; }

            Monitor.Enter(@lock);
            T instance = new T();
            Interlocked.Exchange(ref _instance, instance);
            Monitor.Exit(@lock);
            return _instance;
        }
    }
}
