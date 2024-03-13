using System.Net;
using System.Net.Sockets;

namespace TcpServer;

class TcpServer
{
    static TcpListener listener;

    const int Limit = 5;

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
        int port = 13371;
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

        listener = new TcpListener(ipAddress, port);
        listener.Start();

        for (int i = 0; i < Limit; i++)
        {
            Thread t = new Thread(Service);
            t.Start();
        }
    }

    public static void Service()
    {
        Socket soc = listener.AcceptSocket();

        Stream s = new NetworkStream(soc);
        StreamReader sr = new StreamReader(s);
        StreamWriter sw = new StreamWriter(s);
        sw.AutoFlush = true;
        
        while (true)
        {
            string? request = sr.ReadLine();

            
            if (request is null ||request == "stop")
            {
                s.Close();
                break;
            }
            
            try
            {
                string job = Countries[request];
                sw.WriteLine(job);
            }
            catch
            {
                sw.WriteLine("No such country");
            }
        }

        soc.Close();
    }
}