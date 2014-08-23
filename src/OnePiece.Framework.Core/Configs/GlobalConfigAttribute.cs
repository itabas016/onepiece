using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GlobalConfigAttribute : Attribute
    {
        public GlobalConfigAttribute()
        {

        }

        public GlobalConfigAttribute(string module, string name, string defaultValue)
        {
            this.Module = module;
            this.Name = name;
            this.DefaultValue = defaultValue;
        }

        public string Module { get; set; }

        public string Name { get; set; }

        public string DefaultValue { get; set; }
    }
}
