using System;
using System.Collections.Generic;

namespace MultiFilling
{
    public class ChannelNode : IFetchingParams
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Descriptor { get; set; }
        public string IpAddr { get; set; }

        public int LinkType { get; set; }
        public int Comport { get; set; }
        public int Baudrate { get; set; }
        public string Parity { get; set; }
        
        public List<RiserNode> Risers { get; set; }
        public int FetchTime { get; set; }
        public int SendTimeout { get; set; }
        public int ReceiveTimeout { get; set; }

        public bool Active { get; set; }
        public bool Linked { get; set; }
        public DateTime NextFetching { get; set; }
        public long TotalRequests { get; set; }
        public long TotalErrors { get; set; }
        public int BarometerValue { get; set; }
        public int MarginalLimit { get; set; }
        public int FailLimit { get; set; }
        public FetchingStatus Status { get; set; }
        public TimeSpan TimeMarginal { get; set; }
        public TimeSpan TimeFail { get; set; }
        public RiserAddress Address { get; set; }
        public void UpdateData(ushort[] hregs, int start, int count, bool remoted = false)
        {
            // stub
        }

        public int Overpass { get; set; }
        public int Way { get; set; }
        public string WayFine { get; set; }
        public string Product { get; set; }
        public string ProductFine { get; set; }
        public int[] RisersRange { get; set; }
        public string RisersRangeFine { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}