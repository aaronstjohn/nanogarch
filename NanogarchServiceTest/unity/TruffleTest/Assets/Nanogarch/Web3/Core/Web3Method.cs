using System;
using Newtonsoft.Json;
using Promises;
using Nanogarch.Web3.Unity;
namespace Nanogarch.Web3.Core {
    public class Web3Method<T> 
    {
        protected uint id;
        protected string json;
        protected string method;
        protected Type type;
        
        public Web3Method(uint id, string json, string method )
        {
            this.id = id;
            this.json = json;
            this.method = method;
            this.type = typeof(Web3Response<T>);
            
        }
        public Promise<T> Execute()
        {
            Web3Request request = new Web3Request(id,json,method,new string[]{});
            return Web3Service.getService().Request(request.ToJson()).Then<T>(Convert);
            
        }
        protected T Convert(string result){
            
            return JsonConvert.DeserializeObject<Web3Response<T>>(result).result;
            
        }
    }
    public class Web3Method2<U,V,T>:Web3Method<T>
    {
        public  Web3Method2(uint id, string json, string method ):base(id,json,method){}
        public Promise<T> Execute(U arg0, V arg1)
        {
            Web3Request request = new Web3Request(id,json,method,new string[]{arg0.ToString(),arg1.ToString()});
            return Web3Service.getService().Request(request.ToJson()).Then<T>(Convert);
        }
    }
}