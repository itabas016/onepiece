using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public interface IGlobalConfigService
    {
        IGlobalConfigItem GetConfigItem(string key);

        IGlobalConfigItem Create(string module, string key, string name, string value);
    }
}
