using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Collections.Specialized;

namespace motiv_client
{
    static class Post
    {
        static internal void Send(int keydowns, int clicks)
        {
            WebClient wc = new WebClient();
            NameValueCollection ps = new NameValueCollection();
            ps.Add("password", Properties.Settings.Default.Password);
            ps.Add("keydowns", keydowns.ToString());
            ps.Add("clicks", clicks.ToString());
            wc.UploadValues(Properties.Settings.Default.ServerUrl, ps);
        }
    }
}
