using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace fail.hardware.Hotzone.Client
{ 
    public class NfcScanner
    {
        static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Endpoint { get; set; }


        public static NfcScanner FromJson(string json)
        {
            return JsonConvert.DeserializeObject<NfcScanner>(json, SerializerSettings);
        }

        public override string ToString()
        {
            return $"DeviceId: {DeviceId}, DeviceName: {DeviceName}, Endpoint: {Endpoint}";
        }
    }

}
