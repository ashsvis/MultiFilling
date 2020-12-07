namespace MultiFilling.SystemStatus
{
    public interface IUserControlMisc
    {
        int DisplayIndex { get; set; }
        void Loaded();
        void Unload();
    }
}