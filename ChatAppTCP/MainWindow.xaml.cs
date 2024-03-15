using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatAppTCP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IPAddress ip =IPAddress.Parse("127.0.0.1");
        public int port = 27001;
        public IPEndPoint ServerEp {  get; set; }
       public TcpClient tcpClient {  get; set; }
        public NetworkStream? clientdata { get; set; }
        public BinaryReader? reader { get; set; }
        public BinaryWriter? writer { get; set; }

        public List<string> AllUser=new List<string>();
        public string _name {  get; set; }
        public MainWindow(string name)
        {
            _name=name;
            ServerEp=new IPEndPoint(ip, port);
            tcpClient = new TcpClient();
            tcpClient.Connect(ServerEp);
            
            clientdata= tcpClient.GetStream();
            reader = new BinaryReader(clientdata);
            writer = new BinaryWriter(clientdata);
            writer.Write(name);
            Task.Run(() => { while (true) { writer.Write("online"); } });
            InitializeComponent();
        }
        
        


    }
}