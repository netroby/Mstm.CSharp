using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Thread
{
    public enum LockType
    {
        Monitor = 1,
        Mutex = 2,
        EventWaitHandle,
        AutoResetEvent,
        ManualResetEvent,
        Semaphore,
        ManualResetEventSlim,
        ReaderWriterLockSlim,
        CountdownEvent,
        Barrier
    }
}
