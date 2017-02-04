
using UnityEngine;
using Promises;
using Web3;
using System;
using Newtonsoft.Json;
// using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// class Web3Request
// {
// 	public uint id;
// 	public string jsonrpc;
// 	public string method;
	
// 	[JsonPropertyAttribute("params")]
// 	public string[] web3params;

// 	public Web3Request(uint id,string jsonrpc,string method,string[] web3params)
// 	{
// 		this.id = id;
// 		this.jsonrpc = jsonrpc;
// 		this.method= method;
// 		this.web3params = web3params;
// 	}
// 	public string ToJson()
// 	{
// 		return JsonConvert.SerializeObject(this);
// 	}
// }
// class EthAccountsResponse
// {
// 	public uint id;
// 	public string jsonrpc;
// 	public string[] result;
// }
class Web3Response<T>
{
	public uint id;
	public string jsonrpc;
	public T result;
}
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
public sealed class BlockParameter {

    private readonly string name;
    private readonly uint value;
	private readonly bool isValue;
    public static readonly BlockParameter LATEST = new BlockParameter(0,"latest");
    public static readonly BlockParameter EARLIEST = new BlockParameter(0,"earliest");
     public static readonly BlockParameter PENDING = new BlockParameter(0,"pending");      

    public BlockParameter(uint value, String name="none"){
		this.name = name;
        this.value = value;
		if(name == "none")
			isValue = true;
    }

    public override string ToString(){
		if(isValue)
			return value.ToString();
		return name;
        // return name;
    }

}
public class AddressConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        Address address = (Address)value;

        writer.WriteValue(address.address);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        Address address = new Address();
        address.address = (string)reader.Value;

        return address;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Address);
    }
}
[JsonConverter(typeof(AddressConverter))]
public sealed class Address 
{
	public  string address;
	
	public override string ToString(){return address;}
	
}

 
public static class Eth
{
	public static readonly Func< Promise<Address[]>> Accounts = new Func<Promise<Address[]>>( (new Web3Method<Address[]>(1,"2.0","eth_accounts")).Execute);
	public static readonly Func< Promise<Address>> Coinbase = new Func<Promise<Address>>( (new Web3Method<Address>(64,"2.0","eth_coinbase")).Execute);
	public static readonly Func< Promise<bool>> Mining = new Func<Promise<bool>>( (new Web3Method<bool>(71,"2.0","eth_mining")).Execute);
	public static readonly Func< Promise<uint>> Hashrate = new Func<Promise<uint>>( (new Web3Method<uint>(71,"2.0","eth_hashrate")).Execute);
	public static readonly Func< Promise<uint>> GasPrice = new Func<Promise<uint>>( (new Web3Method<uint>(73,"2.0","eth_gasPrice")).Execute);
	public static readonly Func< Address,BlockParameter,Promise<uint>> BlockNumber = new Func<Address,BlockParameter,Promise<uint>>( (new Web3Method2<Address,BlockParameter,uint>(1,"2.0","eth_getBalance")).Execute);

}
public class GetAccounts : MonoBehaviour {

	 void Awake() {
        MainThreadDispatcher.Init();
    }
	// Use this for initialization
	void Start () {
		
	}
	public void GetEthAccounts()
	{
		Debug.Log("NEED TO HANDLE CUSTOM CONVERSION OF ADDRESSES");
		Eth.Accounts()
			.Then<Nothing>((Address[] response)=>{
				foreach( Address addy in response )
				{
					Debug.Log("FOUND ADDRESS: "+ addy);
				}
				return Nothing.AtAll;
			});
	}
	// Update is called once per frame
	void Update () {
		
	}
}
