using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core.Security
{
    public class SignatureRepository : SingletonBase<SignatureRepository>
    {
        public SignatureRepository()
        {
            SecretKeys = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            SecretKeys["yl"] = "4c6f1851f6a7496c86ffac67540ae9f5";

            /* reserved keys
             * 
             * ea9f0c8855984c578cbe6b232282bf5e
             * febb604864ce49278a5e21ae2453ba95
             * 
             */
        }

        public Dictionary<string, string> SecretKeys { get; private set; }

        public string TryGet(string appType)
        {
            if (!appType.IsNullOrEmpty() && this.SecretKeys.ContainsKey(appType))
            {
                return this.SecretKeys[appType];
            }

            return string.Empty;
        }
    }
}
