using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AES_CHAT
{
    class Program
    {
        static TcpClient client;
        static NetworkStream stream;
        static char split = '|';
        static string username;
        static string password;
        static string message;
        static string[] host;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Client";

            if (args.Length >= 1 && args[0].Contains(":"))
            {
                host = args[0].Split(':');
            }
            else
            {
                host = RequestInput("Server IP:PORT (format: IP:PORT): ").Split(':');
            }

            if (host.Length != 2 || !int.TryParse(host[1], out _))
            {
                Console.Error.WriteLine("Invalid server address format. Please use IP:PORT.");
                Console.ReadKey();
                return;
            }

            try
            {
                client = new TcpClient(host[0], Convert.ToInt32(host[1]));
                stream = client.GetStream();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Unable to connect to the server: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connected!\n");
            Console.ResetColor();

            username = args.Length >= 2 && !string.IsNullOrEmpty(args[1])
                ? args[1]
                : RequestInput("Username: ");
            password = args.Length >= 3 && !string.IsNullOrEmpty(args[2])
                ? args[2]
                : RequestInput("Server password: ");

            Console.Title = $"Client | {username} | Connected";

            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            StartMessageLoop();
        }

        static string RequestInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static void StartMessageLoop()
        {
            while (true)
            {
                Console.Write(">");
                string message = Console.ReadLine();

                if (!string.IsNullOrEmpty(message))
                {
                    string encrypted_message = new AES().Encrypt($"{username}{split}{message}", password);
                    byte[] data = Encoding.UTF8.GetBytes(encrypted_message);

                    try
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error sending message: {ex.Message}");
                        Console.ResetColor();
                        break;
                    }
                }
            }
        }

        static void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytes = stream.Read(buffer, 0, buffer.Length);
                if (bytes == 0) break;
                try
                {
                    message = new AES().Decrypt(Encoding.UTF8.GetString(buffer, 0, bytes), password);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{ message.Split(split)[0]}");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($": {message.Split(split)[1]}");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n>");
                }
                catch (Exception) { };
                
                
            }
        }
    }
}
