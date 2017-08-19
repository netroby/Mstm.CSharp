using Mstm.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.CLR
{
    public class AppDomainUtil
    {
        public static AppDomainInfo GetMonitorInfo(AppDomain domain)
        {
            if (AppDomain.MonitoringIsEnabled == false)
            {
                AppDomain.MonitoringIsEnabled = true;
            }
            domain = domain ?? AppDomain.CurrentDomain;
            AppDomainInfo appDomainInfo = new Model.AppDomainInfo();
            appDomainInfo.MonitoringSurvivedMemorySize = domain.MonitoringSurvivedMemorySize;
            appDomainInfo.MonitoringTotalAllocatedMemorySize = domain.MonitoringTotalAllocatedMemorySize;
            appDomainInfo.MonitoringSurvivedProcessMemorySize = AppDomain.MonitoringSurvivedProcessMemorySize;
            appDomainInfo.MonitoringTotalProcessorTime = domain.MonitoringTotalProcessorTime;
            return appDomainInfo;

        }
    }
}
