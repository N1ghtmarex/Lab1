using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpServer;

class UdpServer
{
    static readonly Dictionary<string, string> Countries =
        new()
        {
            {"Russia", "Moscow"},
            {"USA", "Washington, D.C."},
            {"China", "Beijing"},
            {"India", "New Delhi"},
            {"Brazil", "Brasília"},
            {"Germany", "Berlin"},
            {"France", "Paris"},
            {"Japan", "Tokyo"},
            {"South Korea", "Seoul"},
            {"Italy", "Rome"}
        };

    public static void Main()
    {
        UdpClient udpClient = new UdpClient(13371);
        Console.WriteLine("Server started");
        IPEndPoint? ep = null;

        while (true)
        {
            byte[] rdata = udpClient.Receive(ref ep);
            string request = Encoding.ASCII.GetString(rdata);
            string response;

            if (request == "stop")
            {
                break;
            }

            try
            {
                response = Countries[request];
            }
            catch
            {
                response = "No such country";
            }

            byte[] sdata = Encoding.ASCII.GetBytes(response);

            udpClient.Send(sdata, sdata.Length, ep);
        }
    }
}