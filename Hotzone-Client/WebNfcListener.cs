using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fail.hardware.Hotzone.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace fail.hardware.NfcClient
{
    public class WebNfcListener
    {
        private readonly Socket _ioHandler;
        private string _endpoint;
        private readonly string _address;

        public event Action OnConnected;
        public event Action OnError;
        public event Action<NfcScanner> OnScannerRegistered;
        public event Action<NfcScanner> OnScannerDisconnected;
        public event Action<NfcScan> OnScanned;


        public List<NfcScanner> Scanners = new List<NfcScanner>();

        public WebNfcListener(string ident)
        {
            if (!ident.Contains("@"))
                throw new ArgumentException("Invalid ident - missing @");
            var parts = ident.Split('@');
            if(parts.Length != 2)
                throw new ArgumentException("Invalid ident - invalid length");
            _endpoint = parts[0];
            _address = parts[1];

            _ioHandler = IO.Socket($"http://{_address}", new IO.Options {AutoConnect = false, Reconnection = true});

            _ioHandler.On(Socket.EVENT_CONNECT, (obj) =>
            {
                OnConnected?.Invoke();
            });

            _ioHandler.On("scanner.registered", (obj) =>
            {
                var scanner = NfcScanner.FromObject(obj);
                if (!Scanners.Exists(x => x.DeviceId == scanner.DeviceId))
                {
                    Scanners.Add(scanner);
                    OnScannerRegistered?.Invoke(scanner);
                }
            });
            _ioHandler.On("scanner.disconnected", (obj) =>
            {
                dynamic o = obj;
                string deviceId = o.device_id;
                var scanner = Scanners.FirstOrDefault(x => x.DeviceId == deviceId);
                if (scanner != null)
                    Scanners.Remove(scanner);
                OnScannerDisconnected?.Invoke(scanner);
            });
            _ioHandler.On("scanner.scanned", (obj) =>
            {
                dynamic o = obj;
                string deviceId = o.device_id;
                var scanner = Scanners.FirstOrDefault(x => x.DeviceId == deviceId);

                var s = new NfcScan {Scanner = scanner, CardId = ((dynamic)obj).card_id };
                OnScanned?.Invoke(s);
                
            });

        }



        public void Connect()
        {
            _ioHandler.Connect();
            var payload = new
            {
                name = _endpoint
            };
            _ioHandler.Emit("endpoint.register", JObject.FromObject(payload));
        }
    }
}
