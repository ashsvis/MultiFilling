using System;
using System.ServiceModel;

namespace MultiFilling.EventServer
{
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void PropertyUpdated(DateTime servertime, string category, string pointname, string propname, string value);

        [OperationContract(IsOneWay = true)]
        void FileBlockReceived(Guid fileId, long length, int block, int size, byte[] buffer, byte[] md5);
    }
}