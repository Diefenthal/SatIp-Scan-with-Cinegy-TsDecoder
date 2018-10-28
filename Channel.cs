namespace SatIp
{
    internal class Channel
    {
        public object ServiceType { get; internal set; }
        public string ServiceName { get; internal set; }
        public string ServiceProvider { get; internal set; }
        public object ServiceId { get; internal set; }
        public object ProgramClockReferenceId { get; internal set; }
        public object VideoPid { get; internal set; }
        public object AudioPid { get; internal set; }
        public object AC3Pid { get; internal set; }
        public object TTXPid { get; internal set; }
        public object SubTitlePid { get; internal set; }
        public short EAC3Pid { get; internal set; }
        public short AACPid { get; internal set; }
        public short DTSPid { get; internal set; }
    }
}