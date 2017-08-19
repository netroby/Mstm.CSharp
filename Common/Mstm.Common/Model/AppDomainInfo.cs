using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Model
{
    public class AppDomainInfo
    {
        public long MonitoringSurvivedMemorySize { get; set; }

        public long MonitoringTotalAllocatedMemorySize { get; set; }

        public long MonitoringSurvivedProcessMemorySize { get; set; }

        public TimeSpan MonitoringTotalProcessorTime { get; set; }
    }
}
