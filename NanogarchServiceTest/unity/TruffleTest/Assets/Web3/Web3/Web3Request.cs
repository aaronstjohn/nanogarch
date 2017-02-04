using Newtonsoft.Json;
namespace Web3 {
    public class Web3Request
    {
        public uint id;
        public string jsonrpc;
        public string method;
        
        [JsonPropertyAttribute("params")]
        public string[] web3params;

        public Web3Request(uint id,string jsonrpc,string method,string[] web3params)
        {
            this.id = id;
            this.jsonrpc = jsonrpc;
            this.method= method;
            this.web3params = web3params;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}