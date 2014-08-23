using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public interface IRequestRepository
    {
        NameValueCollection Header { get; }

        NameValueCollection QueryString { get; }

        IDictionary HttpContextItems { get; }

        string UserHostName { get; }

        string RawUrl { get; }

        string ClientIP { get; }

        string SessionId { get; }

        string UserAgent { get; }

        string HttpMethod { get; }

        /// <summary>
        /// Request.Url.query
        /// </summary>
        string QueryUrl { get; }

        Byte[] GetRequestPostStream();

        Stream GetInputStream();

        string PostedDataString { get; }

        string GetValueFromHeadOrQueryString(string key);
    }
}
