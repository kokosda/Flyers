using System;
using System.IO;
using System.Text;
using System.Threading;

namespace FlyerMe
{
    public sealed class WebRequestDataHandler
    {
        public WebRequestDataHandler(Stream responseStream, Boolean modeBinary)
        {
            ResponseStream = responseStream;
            Buffer = new Byte[1024];
            StringBuilder = new StringBuilder();
            ModeBinary = modeBinary;

            if (ModeBinary)
            {
                Bytes = new Byte[0];
            }
        }

        public Stream ResponseStream { get; set; }

        public Byte[] Buffer { get; private set; }

        public StringBuilder StringBuilder { get; private set; }

        public Boolean ModeBinary { get; private set; }

        public Byte[] Bytes { get; private set; }

        public Exception Exception { get; set; }

        public volatile Boolean IsReading;

        public void ReadBufferIntoBytes(Int64 readBytesCount)
        {
            if (Bytes.Length + readBytesCount <= maxBytesLength)
            {
                if (readBytesCount > 0)
                {
                    var tempBytes = new Byte[Bytes.Length + readBytesCount];

                    Array.Copy(Bytes, tempBytes, Bytes.Length);
                    Array.Copy(Buffer, 0, tempBytes, Bytes.Length, readBytesCount);
                    Bytes = tempBytes;
                }
            }
            else
            {
                throw new Exception(String.Format("Response length is larger then possible (max bytes allowed {0}).", maxBytesLength.ToString()));
            }
        }

        #region private

        private const Int64 maxBytesLength = 32 * 1024 * 1024;

        #endregion
    }
}