using System.ServiceModel.Channels;
using Microsoft.WindowsAzure.Storage;

namespace AzureStorageDemo.Blob
{
    public class WCFBufferManagerAdapter : IBufferManager
    {
        private int defaultBufferSize = 0;

        public WCFBufferManagerAdapter(BufferManager manager, int defaultBufferSize)
        {
            this.Manager = manager;
            this.defaultBufferSize = defaultBufferSize;
        }

        public BufferManager Manager { get; internal set; }

        public void ReturnBuffer(byte[] buffer)
        {
            this.Manager.ReturnBuffer(buffer);
        }

        public byte[] TakeBuffer(int bufferSize)
        {
            return this.Manager.TakeBuffer(bufferSize);
        }

        public int GetDefaultBufferSize()
        {
            return this.defaultBufferSize;
        }
    }
}
