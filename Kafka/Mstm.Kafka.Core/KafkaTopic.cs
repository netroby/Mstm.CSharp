using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Kafka.Core
{

    /// <summary>
    /// 主题
    /// </summary>
    public class KafkaTopic
    {
        public short ErrorCode { get; set; }
        public string Name { get; set; }
        public List<KafkaPartition> Partitions { get; set; }

    }
}
