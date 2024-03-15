

using System.Net;
using System.Net.Sockets;

IPAddress ip = IPAddress.Parse("127.0.0.1");
var port = 27001;

var listenerEP= new IPEndPoint(ip, port);

TcpListener listener = new TcpListener(listenerEP);

listener.Start();
Console.WriteLine($"Server is running on {listener.LocalEndpoint}");

Dictionary<TcpClient ,string> allclients= new Dictionary<TcpClient ,string>();

while (true)
{
    var client = listener.AcceptTcpClient();
    Console.WriteLine($"Client Connected {client.Client.RemoteEndPoint}");
    NetworkStream? clientdata = client.GetStream();
    BinaryReader? reader = new BinaryReader(clientdata);
    var name = reader.ReadString();
    allclients.Add(client, name);
    
    //handshake
    _ = Task.Run(() =>
    {
        NetworkStream? clientdata = client.GetStream();
        BinaryReader? reader = new BinaryReader(clientdata);
        BinaryWriter? writer = new BinaryWriter(clientdata);
        while (true)
        {
            try
            {
                while (true)
                {
                    reader.ReadString();
                }

            }
            catch (Exception)
            {

               allclients.Remove(client);
            }
            
        }

    });

    _ = Task.Run(() =>
    {

    });

    _ = Task.Run(() =>
    {
        NetworkStream? clientdata=client.GetStream();
        BinaryReader? reader=new BinaryReader(clientdata);
        BinaryWriter? writer=new BinaryWriter(clientdata);

        while (true)
        {
        var  message = reader.ReadString();
        Console.WriteLine($"Client {message}");
        }

    });

}

