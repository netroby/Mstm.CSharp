using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Lock.Core
{
    public abstract class AbstractLockProvider : ILockProvider
    {
        public string IdentityName { get; private set; }

        public AbstractLockProvider(string identityName)
        {
            IdentityName = identityName;
        }

        public abstract ILockerHandle Lock();

        public abstract void UnLock(ILockerHandle handler);
    }
}
