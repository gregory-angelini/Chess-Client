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
        public int CurrentGameId { get; set; } = -1;

        public Client(string host)
        {
            this.host = host;
        }

        // example: RequestPost("players", "", Player, callback)
        async Task RequestPost<T1, T2>(string entity, string method, T1 entry, Action<T2> callback)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = host + entity + method;

                string entryJSON = JsonConvert.SerializeObject(entry);
                StringContent entryContent = new StringContent(entryJSON);
                entryContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (HttpResponseMessage response = await client.PostAsync(requestUrl, entryContent))
                {
                    using (HttpContent content = response.Content)
                    {
                        string resultJSON = await content.ReadAsStringAsync();
                        T2 result = JsonConvert.DeserializeObject<T2>(resultJSON);
                        callback?.Invoke(result);
                    }
                }
            }
        }

        async Task RequestGet<T>(string entity, string method, Action<T> callback)
        {
            string requestUrl = host + entity + method;

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(requestUrl))
                {
                    using (HttpContent content = response.Content)
                    {
                        string resultJSON = await content.ReadAsStringAsync();
                        T result = JsonConvert.DeserializeObject<T>(resultJSON);
                        callback?.Invoke(result);
                    }
                }
            }
        }


        // returns the player with player.GUID or creates a new one
        public async Task GetPlayer(Player player, Action<PlayerInfo> callback)
        {
            await RequestPost("players", "", player, callback);
        }

        public async Task MakeMove(string move, Action<GameState> callback)
        {
            if (CurrentGameId != -1)
            {
                await RequestGet("moves", "/" + CurrentGameId.ToString() + "/" + move, callback);
            }
        }

        // returns a game with 'wait' status; we can create a new game or join an existing game
        public async Task FindGame(Action<GameInfo> callback)
        {
            await RequestGet("games", "", callback);
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
