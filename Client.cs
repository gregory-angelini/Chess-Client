using System;
using System.IO;
using System.Net;


namespace ChessClient
{
    public class Client
    {
        public string host { get; private set; }
        public string user { get; private set; }

        public Client(string host, string user)
        {
            this.host = host;
            this.user = user;
        }

        string CallServer(string param = "")
        {
            WebRequest request = WebRequest.Create(host + "/" + param);
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public void GetCurrentGame()
        {
            Console.WriteLine(CallServer("Games"));
        }
    }
}
