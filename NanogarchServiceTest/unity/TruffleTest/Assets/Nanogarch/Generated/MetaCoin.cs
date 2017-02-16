
using System;
using Promises;
using Nanogarch.Web3.Core;
using Nanogarch.Web3.Unity;
using Nethereum.Hex.HexTypes;
namespace Nanogarch.Web3 { 
    public class Web3ContractMethod1<U,T>:Web3Method1<U,T>
    {
        Contract c; 
        public  Web3ContractMethod1(uint id, string json, string method ):base(id,json,method)
        {}
        override public Promise<T> Execute(U arg0)
        {
            Transaction t  = new Transaction();
            t.To = c.deployedAddress.ToString();
            t.From = Web3Service.getService().CurrentAccount.ToString();
            t.GasPrice = new HexBigInteger(4);
            t.Gas = new HexBigInteger(5);
            t.Value = new HexBigInteger(2);
            Web3Request request = new Web3Request(id,json,method,new string[]{arg0.ToString()});
            return Web3Service.getService().Request(request.ToJson()).Then<T>(Convert);
        }
       
    }
    public class Contract
    {
        public Address deployedAddress;
        public Contract(Address deployAddress)
        {
            this.deployedAddress=deployAddress;
        }
        public Func<U,Promise<T>> MethodDelegate<U,T>()
        {
            
            
            return new Func<U,Promise<T>>( (new Web3Method1<U,T>(1,"2.0","eth_call")).Execute);

        }
        // ABI abi;
    }
    public class MetaCoin : Contract
    {
        public  Func<Address, Promise<uint>> GetBalanceInEth = new Func<Address,Promise<uint>>( (new Web3Method1<Address,uint>(1,"2.0","eth_accounts")).Execute);
        // public Func< Promise<Address>> Coinbase = new Func<Promise<Address>>( (new Web3Method<Address>(64,"2.0","eth_coinbase")).Execute);
        public MetaCoin(Address deployAddress):base(deployAddress)
        {
            // GetBalanceInEth=new Func<Address,Promise<uint>>( (new Web3Method1<Address,uint>(1,"2.0","eth_accounts")).Execute);
            GetBalanceInEth = new Web3Method1<Address,uint>(1,"2.0","eth_accounts").Delegate;
        }    
    }
}
// contract MetaCoin {
// 	mapping (address => uint) balances;

// 	event Transfer(address indexed _from, address indexed _to, uint256 _value);

// 	function MetaCoin() {
// 		balances[tx.origin] = 10000;
// 	}
//  	/**@dev Calculates a rectangle's surface and perimeter.
//      */
// 	function sendCoin(address receiver, uint amount) returns(bool sufficient) {
// 		if (balances[msg.sender] < amount) return false;
// 		balances[msg.sender] -= amount;
// 		balances[receiver] += amount;
// 		Transfer(msg.sender, receiver, amount);
// 		return true;
// 	}

// 	function getBalanceInEth(address addr) returns(uint){
// 		return ConvertLib.convert(getBalance(addr),2);
// 	}

// 	function getBalance(address addr) returns(uint) {
// 		return balances[addr];
// 	}
// }