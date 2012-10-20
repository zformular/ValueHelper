using System;
using System.Web;

namespace WebHelper.ValueUpload.Infrastructure
{
    internal class StaticWorkerRequest : HttpWorkerRequest
    {
        private readonly HttpWorkerRequest workerRequest;
        private readonly Byte[] buffer;

        public StaticWorkerRequest(HttpWorkerRequest request, Byte[] buffer)
        {
            workerRequest = request;
            this.buffer = buffer;
        }

        public override int ReadEntityBody(byte[] buffer, int size)
        {
            return 0;
        }

        public override int ReadEntityBody(byte[] buffer, int offset, int size)
        {
            return 0;
        }

        public override byte[] GetPreloadedEntityBody()
        {
            return buffer;
        }

        public override int GetPreloadedEntityBody(byte[] buffer, int offset)
        {
            Buffer.BlockCopy(buffer, 0, buffer, offset, buffer.Length);
            return buffer.Length;
        }

        public override int GetPreloadedEntityBodyLength()
        {
            return buffer.Length;
        }

        public override int GetTotalEntityBodyLength()
        {
            return buffer.Length;
        }

        public override string GetKnownRequestHeader(int index)
        {
            return index == HeaderContentLength
                ? "0" : workerRequest.GetKnownRequestHeader(index);
        }

        public override void EndOfRequest()
        {
        }

        public override void FlushResponse(bool finalFlush)
        {
        }

        public override string GetHttpVerbName()
        {
            return null;
        }

        public override string GetHttpVersion()
        {
            return null;
        }

        public override string GetLocalAddress()
        {
            return null;
        }

        public override int GetLocalPort()
        {
            return 0;
        }

        public override string GetQueryString()
        {
            return null;
        }

        public override string GetRawUrl()
        {
            return null;
        }

        public override string GetRemoteAddress()
        {
            return null;
        }

        public override int GetRemotePort()
        {
            return 0;
        }

        public override string GetUriPath()
        {
            return null;
        }

        public override void SendKnownResponseHeader(int index, string value)
        {
        }

        public override void SendResponseFromFile(IntPtr handle, long offset, long length)
        {
        }

        public override void SendResponseFromFile(string filename, long offset, long length)
        {
        }

        public override void SendResponseFromMemory(byte[] data, int length)
        {
        }

        public override void SendStatus(int statusCode, string statusDescription)
        {
        }

        public override void SendUnknownResponseHeader(string name, string value)
        {
        }

    }
}
