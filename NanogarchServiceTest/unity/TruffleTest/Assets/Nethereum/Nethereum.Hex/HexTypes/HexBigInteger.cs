// using System.Numerics;
using Nethereum.Hex.HexConvertors;
using Newtonsoft.Json;
using Org.BouncyCastle.Math;
namespace Nethereum.Hex.HexTypes
{
    [JsonConverter(typeof(HexRPCTypeJsonConverter<HexBigInteger, BigInteger>))]
    public class HexBigInteger:HexRPCType<BigInteger>
    {
       

        public HexBigInteger(string hex) : base(new HexBigIntegerBigEndianConvertor(), hex)
        {
           
        }

        public HexBigInteger(BigInteger value) : base(value, new HexBigIntegerBigEndianConvertor())
        {

        }

        // public HexBigInteger(ulong value) : base(value, new HexBigIntegerBigEndianConvertor())
        // {

        // }

    }
}