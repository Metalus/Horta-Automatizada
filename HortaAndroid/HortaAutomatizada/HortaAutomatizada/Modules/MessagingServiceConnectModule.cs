using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortaAutomatizada
{
    public class MessagingServiceConnectModule
    {
        public string IP { get; private set; }
        public int Port { get; private set; }

        public MessagingServiceConnectModule(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
        }
    }
}
