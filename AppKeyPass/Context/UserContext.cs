using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using AppKeyPass.Models;
using Newtonsoft.Json;

namespace AppKeyPass.Context
{
    public class UserContext
    {
        static string url = "https://localhost:7286/";

        public static async Task<string> Login(string login, string password)
        {
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, url + "login"))
                {
                    Dictionary<string, string> FormData = new Dictionary<string, string>
                    {
                        ["login"] = login,
                        ["password"] = password
                    };
                    FormUrlEncodedContent Content = new FormUrlEncodedContent(FormData);
                    Request.Content = Content;
                    var Response = await Client.SendAsync(Request);
                    if (Response.StatusCode == HttpStatusCode.OK)  // ← исправлено: HttpStatuscode.OK → HttpStatusCode.OK
                    {
                        string sResponse = await Response.Content.ReadAsStringAsync();

                        Auth DataAuth = JsonConvert.DeserializeObject<Auth>(sResponse);
                        return DataAuth.Token;
                    }
                }
            }
            return null;
        }
        public static async Task<string> Register(string login, string password)
        {
            using (HttpClient Client = new HttpClient())
            {
                var Formdata = new Dictionary<string, string>
                {
                    ["login"] = login,
                    ["password"] = password
                };
                var Content = new FormUrlEncodedContent(Formdata);
                var Response = await Client.PostAsync(url + "register", Content);

                if(Response.StatusCode == HttpStatusCode.OK)
                {
                    string sResponse = await Response.Content.ReadAsStringAsync();
                    Auth DataAuth = JsonConvert.DeserializeObject<Auth>(sResponse);
                    return DataAuth.Token;
                }
                else if (Response .StatusCode == (HttpStatusCode)400)
                {
                    string error = await Response.Content.ReadAsStringAsync();
                    MessageBox.Show(error);
                    return null;
                }
            }
            return null;
        }
    }
}