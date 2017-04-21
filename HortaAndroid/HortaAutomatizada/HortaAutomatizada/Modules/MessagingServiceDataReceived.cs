using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortaAutomatizada
{
    public class MessagingServiceDataReceived
    {
        public byte[] Data { get; private set; }
        public MessagingServiceDataReceived(byte[] Data)
        {
            this.Data = Data;
        }
    }
}
