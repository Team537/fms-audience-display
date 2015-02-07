using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Team537.Audience.Commander
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            StreamWriter writer = null;

            while(true)
            {
                Console.WriteLine("enter a command");
                var command = Console.ReadLine();

                if (command.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!client.Connected)
                    {
                        Console.WriteLine("Quitting");
                        break;
                    }

                    Console.WriteLine("Please disconnect first");
                } 
                else if (command.StartsWith("connect", StringComparison.CurrentCultureIgnoreCase))
                {
                    var ipAddress = command.Substring(command.IndexOf(' ') + 1);
                    Console.WriteLine(ipAddress);

                    IPAddress remoteServer;
                    if(!IPAddress.TryParse(ipAddress, out remoteServer))
                    {
                        Console.WriteLine("Invalid address");
                    }

                    try
                    {
                        client.Connect(remoteServer, 8005);
                        writer = new StreamWriter(client.GetStream());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (command.Equals("disconnect", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (client.Connected)
                    {
                        client.Close();
                        writer = null;
                        Console.WriteLine("Disconnected");
                    }
                    else
                    {
                        Console.WriteLine("No connection established");
                    }
                }
                else if (client.Connected && writer != null)
                {
                    writer.WriteLine(command);
                    writer.Flush();
                    Console.WriteLine("command sent");
                }
                else
                {
                    Console.WriteLine("No connection established");
                }
            }
        }
    }
}
