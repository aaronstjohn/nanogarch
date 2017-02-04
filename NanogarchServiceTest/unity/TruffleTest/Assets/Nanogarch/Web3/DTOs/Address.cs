using Newtonsoft.Json;
using System;
namespace Nanogarch.Web3
{
    [JsonConverter(typeof(AddressConverter))]
    public sealed class Address 
    {
        public  string address;
        
        public override string ToString(){return address;}
        
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
}