using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnePiece.Framework.Core
{
    public interface ICookieService
    {
        HttpCookie this[string key] { get; set; }

        string TryGet(string key);
        string TryGet(string key, Func<string> getDefault);

        void Set(string key, string value, int timeoutSeconds);
        void Remove(string key);
    }
}
