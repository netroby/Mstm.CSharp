using Mstm.Lock.Core;
using org.apache.zookeeper;
using org.apache.zookeeper.data;
using org.apache.zookeeper.recipes.@lock;
using Rabbit.Zookeeper;
using Rabbit.Zookeeper.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mstm.Lock.Distributed.ZK
{
    public class ZKLockProvider : AbstractLockProvider
    {
        IZookeeperClient _zkClient;
        private readonly static List<ACL> ACL = ZooDefs.Ids.OPEN_ACL_UNSAFE;
        protected ZKConfig ZKConfig;

        private string NodeName
        {
            get
            {
                string nodeName;
                if (ZKConfig.LockZKRoot.EndsWith("/"))
                {
                    nodeName = ZKConfig.LockZKRoot + this.IdentityName;
                }
                else
                {
                    nodeName = ZKConfig.LockZKRoot + "/" + this.IdentityName;
                }
                return nodeName;
            }
        }

        public ZKLockProvider(string identity)
            : base(identity)
        {
            ZKConfig = ZKConfig.New();
            _zkClient = new ZookeeperClient(ZKConfig.ConnStr);
            if (_zkClient.ExistsAsync(NodeName).Result == false)
            {
                _zkClient.CreateRecursiveAsync(NodeName, ZKConfig.LockZKValue.Bytes());
            }
        }

        public override ILockerHandle Lock()
        {
            WriteLock locker = new WriteLock(_zkClient.ZooKeeper, NodeName, ACL);
            locker.Lock().Wait();
            while (true)
            {
                if (locker.Owner) { return new ZKLockHandle(locker); }
                Thread.Sleep(1);
            }
        }

        public override void UnLock(ILockerHandle handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler), "解锁失败，handler为空，当前NodeName为" + NodeName);
            }
            handler.UnLock();
        }
    }
}
