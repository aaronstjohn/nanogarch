using System;
using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using Org.BouncyCastle.Math;

namespace Nanogarch.Web3
{
    [JsonConverter(typeof (BlockParameterJsonConverter))]
    public class BlockParameter
    {
        public enum BlockParameterType
        {
            latest,
            earliest,
            pending,
            blockNumber
        }

        private BlockParameter(BlockParameterType type)
        {
            ParameterType = type;
        }

        public BlockParameter()
        {
            ParameterType = BlockParameterType.latest;
        }

        public BlockParameter(HexBigInteger blockNumber)
        {
            SetValue(blockNumber);
        }

        // public BlockParameter(ulong blockNumber) : this(new HexBigInteger(blockNumber))
        // {
        // }

        public HexBigInteger BlockNumber { get; private set; }

        public BlockParameterType ParameterType { get; private set; }

        public static BlockParameter CreateLatest()
        {
            return new BlockParameter(BlockParameterType.latest);
        }

        public static BlockParameter CreateEarliest()
        {
            return new BlockParameter(BlockParameterType.earliest);
        }

        public static BlockParameter CreatePending()
        {
            return new BlockParameter(BlockParameterType.pending);
        }


        public void SetValue(BlockParameterType parameterType)
        {
            if (parameterType == BlockParameterType.blockNumber)
                throw new ArgumentException("Please provide the blockNumber when setting the type as blockNumber",
                    "parameterType");
            ParameterType = parameterType;
            BlockNumber = null;
        }

        public void SetValue(string blockNumberHex)
        {
            ParameterType = BlockParameterType.blockNumber;
            BlockNumber = new HexBigInteger(blockNumberHex);
        }

        public void SetValue(HexBigInteger blockNumber)
        {
            ParameterType = BlockParameterType.blockNumber;
            BlockNumber = blockNumber;
        }

        public void SetValue(BigInteger blockNumber)
        {
            ParameterType = BlockParameterType.blockNumber;
            BlockNumber = new HexBigInteger(blockNumber);
        }

        public string GetRPCParam()
        {
            if (ParameterType == BlockParameterType.blockNumber)
            {
                return BlockNumber.HexValue;
            }
            return ParameterType.ToString();
        }
    }
    public class BlockParameterJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var blockParameter = (BlockParameter) value;

            writer.WriteValue(blockParameter.GetRPCParam());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (BlockParameter);
        }
    }
}
// using Newtonsoft.Json;
// using System;
// namespace Nanogarch.Web3
// {
//     public sealed class BlockParameter {

//         private readonly string name;
//         private readonly uint value;
//         private readonly bool isValue;
//         public static readonly BlockParameter LATEST = new BlockParameter(0,"latest");
//         public static readonly BlockParameter EARLIEST = new BlockParameter(0,"earliest");
//         public static readonly BlockParameter PENDING = new BlockParameter(0,"pending");      

//         public BlockParameter(uint value, String name="none"){
//             this.name = name;
//             this.value = value;
//             if(name == "none")
//                 isValue = true;
//         }

//         public override string ToString(){
//             if(isValue)
//                 return value.ToString();
//             return name;
//             // return name;
//         }

//     }
// }