namespace Nanogarch.Web3.Core {

    class Web3Response<T>
    {
        public uint id;
        public string jsonrpc;
        public T result;
    }
}