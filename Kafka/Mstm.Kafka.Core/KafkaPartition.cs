using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Kafka.Core
{
    public class KafkaPartition
    {
        public short ErrorCode { get; set; }
        public List<int> Isrs { get; set; }
        public int LeaderId { get; set; }
        public int PartitionId { get; set; }
        public List<int> Replicas { get; set; }
    }
}
