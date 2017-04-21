using FormsToolkit;
using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HortaAutomatizada.Modules
{
    public class SocketTcp
    {
        public TcpSocketClient socket;
        string IPAddr;
        int Port;

        public bool Connected => socket.ReadStream.CanRead && socket.WriteStream.CanWrite;

        #region Commands
        private Command sendData;
        public Command SendData => 
                 sendData ?? (sendData = new Command(async (p) => await SendDataAsync(p as byte[])));

        private Command disconnect;
        public Command Disconnect =>
                disconnect ?? (disconnect = new Command(async () => await DisconnectAsync()));

        #endregion
        public SocketTcp()
        {
        }

        public async Task<bool> Connect(string IP, int Port)
        {
            try
            {
                this.IPAddr = IP;
                this.Port = Port;
                ReadData();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task ReadData()
        {
            byte[] buffer = new byte[128];
            var ReadStream = socket.ReadStream;
            while (Connected)
            {
                if (await ReadStream.ReadAsync(buffer, 0, buffer.Length) == 0)
                    break;
                else
                    MessagingService.Current.SendMessage(MessageKeys.DataReceived, new MessagingServiceDataReceived(buffer));
                Array.Clear(buffer, 0, buffer.Length);
            }
        }


        private async Task SendDataAsync(byte[] buffer)
        {
            await socket.WriteStream.WriteAsync(buffer, 0, buffer.Length);
        }
        private async Task DisconnectAsync()
        {
            await socket.DisconnectAsync();
        }
    }
}
