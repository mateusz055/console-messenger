using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace console_messenger
{
    public class KeyValuePair
    {
        public Socket socket;
        public byte[] dataBuffer = new byte[1];
    }
}
