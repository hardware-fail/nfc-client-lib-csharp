using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace fail.hardware.NfcClient
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
    }

}
