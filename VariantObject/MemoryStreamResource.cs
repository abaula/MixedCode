using System.IO;
using Microsoft.IO;

namespace VariantObject
{
    public static class MemoryStreamResource
    {
        private static readonly RecyclableMemoryStreamManager StreamManager = new RecyclableMemoryStreamManager();

        public static MemoryStream GetStream()
        {
            return StreamManager.GetStream();
        }
    }
}