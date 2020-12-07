using System;

namespace MultiFilling
{
    public interface IFetchingParams
    {
        bool Active { get; set; }
        bool Linked { get; set; }
        DateTime NextFetching { get; set; }
        long TotalRequests { get; set; }
        long TotalErrors { get; set; }
        int BarometerValue { get; set; }
        int MarginalLimit { get; set; }
        int FailLimit { get; set; }
        FetchingStatus Status { get; set; }
        TimeSpan TimeMarginal { get; set; }
        TimeSpan TimeFail { get; set; }
        void UpdateData(ushort[] hregs, int start, int count, bool remoted = false);
        RiserAddress Address { get; }
    }
}