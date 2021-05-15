using System;
using System.Net;
using System.Net.Sockets;
using Renci.SshNet.Common;

namespace console_messenger
{
    class Server
    {
        Socket server;
        private Socket tempSocket;
        AsyncCallback asyncCallBack;
        IAsyncResult asyn;
        public void Connect(string ipAddr, string port)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, Convert.ToInt32(port));
            server.Bind(ipLocal);//bind to the local IP Address...
            server.Listen(5);
            server.BeginAccept(new AsyncCallback(OnClientConnect), null);
        }
        public void OnClientConnect(IAsyncResult asyn)
        {
            if (server != null)
            {
                tempSocket = server.EndAccept(asyn);
                WaitForData(tempSocket);
                server.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
        }
        public async void WaitForData(Socket soc)
        {

            
            if (asyncCallBack == null)
                asyncCallBack = new AsyncCallback(OnDataReceived);

            KeyValuePair aKeyValuePair = new KeyValuePair();
            aKeyValuePair.socket = soc;

            // now start to listen for incoming data...
            aKeyValuePair.dataBuffer = new byte[soc.ReceiveBufferSize];
            soc.BeginReceive(aKeyValuePair.dataBuffer, 0, aKeyValuePair.dataBuffer.Length,
                         SocketFlags.None, asyncCallBack, aKeyValuePair);


        }
          IAsyncResult OnDataReceived(object sender, EventArgs e)
        {
            
            KeyValuePair aKeyValuePair = (KeyValuePair)asyn.AsyncState;
            int iRx = 0;
            iRx = aKeyValuePair.socket.EndReceive(asyn);
            if (iRx != 0)
            {
                byte[] recv = aKeyValuePair.dataBuffer;
            }
        }
    }
}
