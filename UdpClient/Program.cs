using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientUdp;

class ClientUdp
{
    public static void Main(string[] args)
    {
        UdpClient udpClient = new UdpClient("127.0.0.1", 13371);
        IPEndPoint? ep = null;

        while (true)
        {
            Console.Write("Request:  ");
            string? request = Console.ReadLine();

            if (request == null || request == "stop")
            {
                break;
            }

            byte[] sdata = Encoding.ASCII.GetBytes(request);
            udpClient.Send(sdata, sdata.Length);

            byte[] rdata = udpClient.Receive(ref ep);
            string response = Encoding.ASCII.GetString(rdata);
            Console.WriteLine("Response: " + response);
        }
    }
}

