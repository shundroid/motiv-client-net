using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Collections.Specialized;
using System.Net.Http;

namespace motiv_client
{
    static class Post
    {
        static internal async Task Send(int keydowns, int clicks)
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new SortedDictionary<string, string>
            {
                { "password", Properties.Settings.Default.Password },
                { "keydowns", keydowns.ToString() },
                { "clicks", clicks.ToString() }
            });
            await client.PostAsync(Properties.Settings.Default.ServerUrl, content);
        }
    }
}
