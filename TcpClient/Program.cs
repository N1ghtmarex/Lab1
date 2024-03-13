using System.Net.Sockets;

namespace ClientTcp;

class ClientTcp
{
    public static void Main(string[] args)
    {
        TcpClient tcpClient = new TcpClient("127.0.0.1", 13371);

        Stream s = tcpClient.GetStream();
        StreamReader sr = new StreamReader(s);
        StreamWriter sw = new StreamWriter(s);
        sw.AutoFlush = true;

        while (true)
        {
            Console.Write("Request:  ");
            string? request = Console.ReadLine();

            if (request == "stop")
            {
                s.Close();
                break;
            }

            sw.WriteLine(request);

            Console.WriteLine("Response: " + sr.ReadLine());
        }

        tcpClient.Close();
    }
}