using System;
using System.Net;
using MultiFilling.EventClient;

namespace MultiFilling
{
    public class StationNode
    {
        public string Name { get; set; }
        public string Descriptor { get; set; }
        public int Index { get; set; }
        public bool ItThisStation { get; set; }
        public bool Enable { get; set; }
        public bool Actived { get; set; }
        public IPAddress Address { get; set; }
        public Guid ClientId { get; set; }
        public ClientConnectionStatus ConnectionStatus { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
