using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace fail.hardware.Hotzone.Client
{ 
    public class NfcScanner
    {

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Endpoint { get; set; }


        public static NfcScanner FromObject(object json)
        {
            dynamic obj = json;
            var o = new NfcScanner
            {
                DeviceId = obj.device_id,
                DeviceName = obj.device_name,
                Endpoint = obj.endpoint
            };
            return o;
        }

        public override string ToString()
        {
            return $"DeviceId: {DeviceId}, DeviceName: {DeviceName}, Endpoint: {Endpoint}";
        }
    }

}
