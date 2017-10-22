using Mstm.Common.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Mstm.Common.Config;
using System.Globalization;

namespace Mstm.Lock.Core
{
    public class LockFactory : Factory<ILockProvider>
    {
        private LockFactory() { }

        protected override BaseProviderConfig Config { get; set; }

        public static ILockProvider GetProvider(string identity, string groupName = null)
        {
            LockFactory instance = new LockFactory();
            instance.Config = LockProviderConfig.New(groupName);
            var provider = instance.GetProviderCore(new object[] { identity }, groupName);
            return provider;
        }

        protected override ILockProvider CreateInstance(Assembly assembly, object[] args)
        {
            var provider = assembly.CreateInstance(Config.ClassFullName, true, BindingFlags.CreateInstance, null, args, CultureInfo.CurrentCulture, null) as ILockProvider;
            return provider;
        }
    }
}
