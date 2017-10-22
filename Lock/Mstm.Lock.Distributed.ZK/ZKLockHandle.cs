using Mstm.Lock.Core;
using org.apache.zookeeper.recipes.@lock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Lock.Distributed.ZK
{
    public class ZKLockHandle : ILockerHandle
    {
        WriteLock _locker;
        public ZKLockHandle(WriteLock locker) => _locker = locker ?? throw new ArgumentNullException(nameof(locker), "当前WriteLock锁为空");
        public void UnLock()
        {
            _locker.unlock();
        }
    }
}
