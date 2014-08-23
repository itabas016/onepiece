using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnePiece.Framework.Core.Security
{
    public class SignatureContext
    {
        public readonly string KEY_SIGN = "sign";
        public const string KEY_SIGN_TYPE = "sign_type";
        public const string KEY_APP_TYPE = "app_type";

        public SignatureContext(IRequestRepository requestRepository, string signKey = "")
            : this(requestRepository, Encoding.UTF8, signKey)
        {
        }

        public SignatureContext(IRequestRepository requestRepository, Encoding encoding, string signKey = "")
            : this(requestRepository.RawUrl, encoding, signKey)
        {
        }

        public SignatureContext(string requestUrl, Encoding encoding, string signKey = "")
        {
            if (!signKey.IsNullOrEmpty()) KEY_SIGN = signKey;

            this.Url = requestUrl;
            this.Encoding = encoding;

            var askIndex = this.Url.IndexOf(ASCII.ASK);
            if (askIndex >= 0)
            {
                if (askIndex != this.Url.Length - 1)
                {
                    this.Query = HttpUtility.ParseQueryString(this.Url.Substring(this.Url.IndexOf(ASCII.ASK) + 1), encoding);

                    this.Method = GetSignatureMethod(this.Query);

                    this.Secret = GetSecretViaAppType(this.Query);
                }
            }
        }

        internal static string GetSecretViaAppType(NameValueCollection nameValueCollection)
        {
            var appType = nameValueCollection[KEY_APP_TYPE];

            var secret = SingletonBase<SignatureRepository>.Instance.TryGet(appType);

            return secret;
        }

        internal static SignatureMethod GetSignatureMethod(NameValueCollection nameValueCollection)
        {
            var method = nameValueCollection[KEY_SIGN_TYPE].ToInt32().ToEnum<SignatureMethod>(SignatureMethod.MD5);

            return method;
        }

        /// <summary>
        /// Encryption method
        /// </summary>
        public SignatureMethod Method { get; set; }

        public NameValueCollection Query { get; set; }

        /// <summary>
        /// Except the sign name and value
        /// </summary>
        public NameValueCollection SortedQuery
        {
            get
            {
                if (_sortedQuery == null && this.Query != null)
                {
                    var sortedKeys = this.Query.AllKeys.Except(new List<string> { KEY_SIGN }, StringComparer.OrdinalIgnoreCase).ToList();
                    sortedKeys.Sort();

                    _sortedQuery = new NameValueCollection();
                    sortedKeys.ForEach(s => _sortedQuery.Add(s, Query[s]));
                }
                return _sortedQuery;
            }

        } private NameValueCollection _sortedQuery;

        /// <summary>
        /// 
        /// </summary>
        public string Secret { get; set; }

        public string Url { get; set; }

        public Encoding Encoding { get; set; }

        public string ClientSignature
        {
            get
            {
                if (this.Query != null)
                    return this.Query[KEY_SIGN];

                return string.Empty;
            }
        }
    }
}
