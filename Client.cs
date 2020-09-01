using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChessClient
{
    public class Client
    {
        string host;

        public Client(string host)
        {
            this.host = host;
        }

        public async Task<Player> GetPlayer(Player player)
        {
            using (HttpClient client = new HttpClient())
            {
                string playerJSON = JsonConvert.SerializeObject(player);
                StringContent queryContent = new StringContent(playerJSON);      
                queryContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
  
                using (HttpResponseMessage response = await client.PostAsync(host + "players", queryContent))
                {
                    using (HttpContent content = response.Content)
                    {
                        playerJSON = await content.ReadAsStringAsync();
                        player = JsonConvert.DeserializeObject<Player>(playerJSON);
                    }
                }
            }
            return player;
        }

        public async Task GetGame()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(host + "games"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string gameJSON = await content.ReadAsStringAsync();

                        Console.WriteLine(gameJSON);
                    }
                }
            }
        }

        /*
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
        */



        /*
        public async void PostPlayer(string url)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("query1", "hi"),
                new KeyValuePair<string, string>("query2", "bye"),
            };

            HttpContent q = new FormUrlEncodedContent(queries);
            
            q = new StringContent("dfd");
            using (HttpClient client = new HttpClient()) 
            {
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string contentStr = await content.ReadAsStringAsync();
                       // HttpContentHeaders headers = content.Headers;
                        Console.WriteLine(contentStr);
                    }
                }
            }
        }
        */
    }
}
