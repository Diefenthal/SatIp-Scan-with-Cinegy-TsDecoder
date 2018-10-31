using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatIp
{
    interface IChannel
    {
        decimal Frequency{ get; set; }
        short ServiceType { get; set; }
        string ServiceName { get; set; }
        string ServiceProvider { get; set; }
        ushort ServiceId { get; set; }
        ushort ProgramClockReferenceId { get;  set; }
        short VideoPid { get;  set; }
        short AudioPid { get;  set; }
        short AC3Pid { get;  set; }
        short EAC3Pid { get;  set; }
        short AACPid { get;  set; }
        short DTSPid { get;  set; }
        short TTXPid { get;  set; }
        short SubTitlePid { get;  set; }
    }
}
