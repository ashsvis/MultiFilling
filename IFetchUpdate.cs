namespace MultiFilling
{
    public delegate void FetchData(string ipAddr, int ipPort, int node, int address, int regcount);
    public delegate void WriteData(int address, int regcount, ushort[] hregs, string[] changelogdata = null);

    public interface IFetchUpdate
    {
        void UpdateData(ushort[] hregs, bool remoted);
        void UpdateTimeout();
        event WriteData OnWrite;
    }
}