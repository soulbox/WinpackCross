

//using System.Net.Sockets;

using System.Net.Sockets;

namespace WinpackCross
{
 
    public  class Ping
    {
        public  delegate void PingHandler(string msg);
        public static event PingHandler PingError;
        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
                PingError?.Invoke($"Host:{hostUri }:{portNumber}\n"+ex.ToString());
                //MessageBox.Show("Error pinging host:'" + hostUri + ":" + portNumber.ToString() + "'");
                return false;
            }
        }


    }
}
