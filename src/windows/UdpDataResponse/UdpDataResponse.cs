using System.IO;
using Windows.Networking.Sockets;
using Windows.Networking;
using System;
using Windows.Foundation;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Text;

namespace UdpDataResponse
{

    public sealed class UdpDataResponse
    {

        public static IAsyncOperation<string> getudpdata(string message, string serverPort)
        {


            var task = Task.Run(async () =>
            {
                string response = "NO_RESPONSE";

                DatagramSocket socket = new DatagramSocket();

                TypedEventHandler<DatagramSocket, DatagramSocketMessageReceivedEventArgs> handler = (sender, args) =>
                {
                    Stream streamIn = args.GetDataStream().AsStreamForRead();
                    StreamReader reader = new StreamReader(streamIn);
                    //Read the message that was received from the UDP echo server.
                    response = reader.ReadLineAsync().Result;
                    reader.Dispose();
                };

                socket.MessageReceived += handler;


                //Bind the socket to the clientPort so that we can start listening for UDP messages from the UDP echo server.
                try
                {

                    using (var stream = await socket.GetOutputStreamAsync(new HostName("255.255.255.255"), serverPort))
                    {
                        using (var writer = new DataWriter(stream))
                        {
                            var data = Encoding.UTF8.GetBytes(message);

                            writer.WriteBytes(data);
                            writer.StoreAsync();
                        }
                    }

                }
                catch (Exception e)
                {
                    response = "PORT_OCCUPIED" + message + " - " + serverPort;
                }


                await Task.Delay(1000);
                socket.Dispose();
                return response;
            });

            return task.AsAsyncOperation();
        }

    }
}
