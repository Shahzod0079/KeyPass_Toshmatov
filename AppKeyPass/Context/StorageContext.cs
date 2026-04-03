using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppKeyPass.Models;
using Newtonsoft.Json;

namespace AppKeyPass.Context
{
    public class StorageContext
    {
        static string url = "https://localhost:7286/storage/";

        public static async Task<List<Storage>> Get()
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Get, url + "get"))
                {
                    Request.Headers.Add("token", MainWindow.Token);
                    var Response = await Client.SendAsync(Request);
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        List<Storage> Storages = JsonConvert.DeserializeObject<List<Storage>>(sResponse);
                        return Storages ?? new List<Storage>();
                    }
                }
            }
            return new List<Storage>();
        }

        public static async Task<Storage> Add(Storage storage)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, url + "add"))
                {
                    Request.Headers.Add("token", MainWindow.Token);
                    string JsonStorage = JsonConvert.SerializeObject(storage);
                    var Content = new StringContent(JsonStorage, Encoding.UTF8, "application/json");
                    Request.Content = Content;
                    var Response = await Client.SendAsync(Request);

                    string responseBody = await Response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Add Response: {Response.StatusCode}, Body: {responseBody}");

                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        Storage Storage = JsonConvert.DeserializeObject<Storage>(sResponse);
                        return Storage;
                    }
                }
            }
            return null;
        }

        public static async Task<Storage> Update(Storage storage)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Put, url + "update"))
                {
                    Request.Headers.Add("token", MainWindow.Token);
                    string JsonStorage = JsonConvert.SerializeObject(storage);
                    var Content = new StringContent(JsonStorage, Encoding.UTF8, "application/json");
                    Request.Content = Content;
                    var Response = await Client.SendAsync(Request);

                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                        Storage Storage = JsonConvert.DeserializeObject<Storage>(sResponse);
                        return Storage;
                    }
                }
            }
            return null;
        }

        public static async Task Delete(int Id)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Delete, url + "delete"))
                {
                    Request.Headers.Add("token", MainWindow.Token);
                    string json = JsonConvert.SerializeObject(Id);
                    var Content = new StringContent(json, Encoding.UTF8, "application/json");
                    Request.Content = Content;
                    var Response = await Client.SendAsync(Request);
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}