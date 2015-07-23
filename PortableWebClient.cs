using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Masterloop.Tools.Net
{
    public class PortableWebClient
    {
        public PortableWebClient()
        {
            StatusCode = HttpStatusCode.Unused;
            StatusDescription = string.Empty;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Accept { get; set; }

        public string ContentType { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string DownloadString(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            if (this.Username != null && this.Password != null)
            {
                request.Credentials = new NetworkCredential(this.Username, this.Password);
            }
            request.Accept = this.Accept;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse) GetResponse(request);
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                this.StatusCode = response.StatusCode;
                this.StatusDescription = response.StatusDescription;
                string data = reader.ReadToEnd();
                return data;
            }
        }

        public string UploadString(string url, string body)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            if (this.Username != null && this.Password != null)
            {
                request.Credentials = new NetworkCredential(this.Username, this.Password);
            }
            request.Accept = this.Accept;
            request.Method = "POST";
            request.ContentType = this.ContentType;
            using (Stream writer = (Stream)GetRequestStream(request))
            {
                byte[] data = Encoding.UTF8.GetBytes(body);
                writer.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)GetResponse(request);
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                this.StatusCode = response.StatusCode;
                this.StatusDescription = response.StatusDescription;
                string data = reader.ReadToEnd();
                return data;
            }
        }

        private WebResponse GetResponse(WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            IAsyncResult asyncResult = request.BeginGetResponse(r => autoResetEvent.Set(), null);
            autoResetEvent.WaitOne();
            return request.EndGetResponse(asyncResult);
        }

        private Stream GetRequestStream(WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            IAsyncResult asyncResult = request.BeginGetRequestStream(r => autoResetEvent.Set(), null);
            autoResetEvent.WaitOne();
            return request.EndGetRequestStream(asyncResult);
        }
    }
}
