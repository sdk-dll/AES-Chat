using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Data;

namespace AES_CHAT_server
{
    class Program
    {
        static List<TcpClient> clients = new List<TcpClient>();
        static TcpListener listener;

        static void Main()
        {
            Console.Title = "Server";
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine($"Running..");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("New connection: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address);
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[{((IPEndPoint)client.Client.RemoteEndPoint).Address}]: " + message);
                    SendToAll(message, client);
                }
            }
            catch
            {
            }
            finally
            {
                clients.Remove(client);
                client.Close();
            }
        }

        static void SendToAll(string message, TcpClient sender)
        {
            foreach (var client in clients)
            {
                if (client != sender)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
