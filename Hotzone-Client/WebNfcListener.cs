using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fail.hardware.Hotzone.Client;
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
        public event Action<NfcScanner> OnRegistered;
        public event Action<NfcScan> OnScan;

        public WebNfcListener(string ident)
        {
            if (!ident.Contains("@"))
                throw new ArgumentException("Invalid ident - missing @");
            var parts = ident.Split('@');
            if(parts.Length != 2)
                throw new ArgumentException("Invalid ident - invalid length");
            _endpoint = parts[0];
            _address = parts[1];

            _ioHandler = IO.Socket(_address);

            _ioHandler.On(Socket.EVENT_CONNECT, (obj) =>
            {
                OnConnected?.Invoke();
            });

            _ioHandler.On("scanner.registered", (obj) =>
            {
                var scanner = NfcScanner.FromJson(obj as string);
                OnRegistered?.Invoke(scanner);
            });
            _ioHandler.On("scanner.scanned", (obj) =>
            {
                var scanner = NfcScanner.FromJson(obj as string);
                
            });

        }



        public void Connect()
        {
            _ioHandler.Connect();
        }
    }
}
