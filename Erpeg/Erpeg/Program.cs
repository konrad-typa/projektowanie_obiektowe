using System.Net;
using Erpeg.Core.GameLoop;

namespace Erpeg
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            int port = 5555;

            if (args.Length > 0 && args[0] == "--server" && args.Length <= 2)
            {
                port = args.Length > 1 ? int.Parse(args[1]) : 5555;
                
                Server server = new Server(port);
                await server.RunAsync();
                return;
            }
            else if (args.Length > 0 && args[0] == "--client" &&  args.Length <= 3)
            {
                IPAddress address;
                try
                {
                    var parts = args.Length > 1 ? args[1].Split(':') : ["127.0.0.1", "5555"];
                    address = IPAddress.Parse(parts[0]);
                            
                    if (parts.Length > 1)
                    {
                        port = int.Parse(parts[1]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                
                Client client = new Client(address, port);
                await client.RunAsync();
                return;
            }
            else if (args.Length > 0)
            {
                Console.WriteLine("Wrong command line arguments");
                return;
            }

            string? mode;
            while (true)
            {
                Console.CursorVisible = false;
                Console.WriteLine("Run as [S] server or [C] client?");
                mode = Console.ReadKey(true).Key.ToString().ToUpper();
                if (mode == "S" || mode == "C")
                    break;
                Console.Clear();
                Console.WriteLine("Wrong input. Try again");
            }
            Console.CursorVisible = true;

            Console.Clear();
            if (mode == "S")
            {
                Server server = new Server();
                await server.RunAsync();
            }
            else if (mode == "C")
            {
                Client client = new Client();
                await client.RunAsync();
            }
        }
    }
}