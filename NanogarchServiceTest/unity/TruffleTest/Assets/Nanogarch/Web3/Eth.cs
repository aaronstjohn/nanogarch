using Promises;
using System;
using Nanogarch.Web3.Core;
namespace Nanogarch.Web3 { 
    public static class Eth
    {
        public static readonly Func< Promise<Address[]>> Accounts = new Func<Promise<Address[]>>( (new Web3Method<Address[]>(1,"2.0","eth_accounts")).Execute);
        public static readonly Func< Promise<Address>> Coinbase = new Func<Promise<Address>>( (new Web3Method<Address>(64,"2.0","eth_coinbase")).Execute);
        public static readonly Func< Promise<bool>> Mining = new Func<Promise<bool>>( (new Web3Method<bool>(71,"2.0","eth_mining")).Execute);
        public static readonly Func< Promise<uint>> Hashrate = new Func<Promise<uint>>( (new Web3Method<uint>(71,"2.0","eth_hashrate")).Execute);
        public static readonly Func< Promise<uint>> GasPrice = new Func<Promise<uint>>( (new Web3Method<uint>(73,"2.0","eth_gasPrice")).Execute);
        public static readonly Func< Address,BlockParameter,Promise<uint>> GetBalance = new Func<Address,BlockParameter,Promise<uint>>( (new Web3Method2<Address,BlockParameter,uint>(1,"2.0","eth_getBalance")).Execute);

        // public static readonly Func< Transaction,BlockParameter,Promise<uint>> Call = new Func<Transaction,BlockParameter,Promise<uint>>( (new Web3Method2<Transaction,BlockParameter,uint>(1,"2.0","eth_call")).Execute);

        
    }
}