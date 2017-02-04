// using System.Numerics;
using Nethereum.Hex.HexConvertors.Extensions;
using Org.BouncyCastle.Math;

namespace Nethereum.Hex.HexConvertors
{
    public class HexBigIntegerBigEndianConvertor: IHexConvertor<BigInteger>
    {  

        public string ConvertToHex(BigInteger newValue)
        {
            // return newValue.ToString(16);
            return newValue.ToHex(false);
        }

        public BigInteger ConvertFromHex(string hex)
        {
            return hex.HexToBigInteger(false);
        }

    }
}