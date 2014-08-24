using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnePiece.Framework.Core
{
    public class RequestRepository : IRequestRepository
    {
        public Byte[] GetRequestPostStream()
        {
            var request = HttpContext.Current.Request;
            Stream s = null;

            if (request.Files.Count > 0)
                s = request.Files[0].InputStream;
            else
                s = request.InputStream;

            if (s == null) return null;

            var bytes = new byte[s.Length];
            s.Read(bytes, 0, (int)s.Length);

            return bytes;
        }

        public Stream GetInputStream()
        {
            var request = HttpContext.Current.Request;
            Stream s = null;

            if (request.Files.Count > 0)
                s = request.Files[0].InputStream;
            else
                s = request.InputStream;

            return s;
        }

        public const string SERVER_VARIABLES_HOST = "HTTP_HOST";

        // for nginx
        public const string SERVER_VARIABLES_X_FORWARDED_FOR = "X-FORWARDED-FOR";
        public const string SERVER_VARIABLES_X_REAL_IP = "X-REAL-IP";

        // for no nginx
        public const string SERVER_VARIABLES_FORWARDED_FOR = "HTTP_X_FORWARDED_FOR";
        public const string SERVER_VARIABLES_ADDR = "REMOTE_ADDR";

        public NameValueCollection Header
        {
            get
            {
                return HttpContext.Current.Request.Headers;
            }
        }

        public string HttpMethod
        {
            get
            {
                return HttpContext.Current.Request.HttpMethod;
            }
        }

        public NameValueCollection QueryString
        {
            get
            {
                return HttpContext.Current.Request.QueryString;
            }
        }

        public IDictionary HttpContextItems
        {
            get { return HttpContext.Current.Items; }
        }

        public string RawUrl
        {
            get
            {
                var rawUrl = HttpContext.Current.Request.RawUrl;

                return rawUrl;
            }
        }

        public string PostedDataString
        {
            get
            {
                //var post = string.Empty;
                //if (HttpContext.Current.Items[HeaderKeys.POST] == null)
                //{
                //    post = WebHelper.GetPostData(HttpContext.Current.Request.InputStream);
                //    HttpContext.Current.Items[HeaderKeys.POST] = post;
                //}
                //else
                //{
                //    post = HttpContext.Current.Items[HeaderKeys.POST].ToString();
                //}
                //return post;
                throw new NotImplementedException();
            }
        }

        public string GetValueFromHeadOrQueryString(string key)
        {
            if (key.IsNullOrEmpty()) return null;

            var headerValue = default(string);
            var queryStringValue = default(string);

            if (Header != null)
            {
                headerValue = Header[key];
            }

            if (headerValue.IsNullOrEmpty() && QueryString != null)
            {
                queryStringValue = QueryString[key];
                if (!queryStringValue.IsNullOrEmpty())
                    headerValue = queryStringValue;
            }

            return headerValue;
        }

        public string UserHostName
        {
            get { return HttpContext.Current.Request.ServerVariables[SERVER_VARIABLES_HOST]; }
        }

        public string QueryUrl
        {
            get { return HttpContext.Current.Request.Url.Query; }
        }

        public string ClientIP
        {
            get
            {
                var serverVariables = HttpContext.Current.Request.ServerVariables;

                // get the ip from ngnix forwarded for
                var ip = serverVariables[SERVER_VARIABLES_X_FORWARDED_FOR];
                // get the ip from ngnix real ip
                if (ip.IsNullOrEmpty()) { ip = serverVariables[SERVER_VARIABLES_X_REAL_IP]; }

                // get the ip from forwarded for if no ngnix
                if (ip.IsNullOrEmpty()) { ip = serverVariables[SERVER_VARIABLES_FORWARDED_FOR]; }
                if (ip.IsNullOrEmpty()) { ip = serverVariables[SERVER_VARIABLES_ADDR]; }

                return ip.TakeLength(32);
            }
        }

        public string SessionId
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return HttpContext.Current.Session.SessionID;
                return string.Empty;
            }
        }

        public string UserAgent
        {
            get { return HttpContext.Current.Request.UserAgent; }
        }
    }
}
