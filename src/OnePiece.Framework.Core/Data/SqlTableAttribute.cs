using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core.Data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlTableAttribute : Attribute
    {
        public SqlTableAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
