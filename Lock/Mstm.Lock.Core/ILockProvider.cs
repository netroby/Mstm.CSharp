using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Lock.Core
{
    public interface ILockProvider
    {
        ILockerHandle Lock();

        void UnLock(ILockerHandle handle);
    }
}
