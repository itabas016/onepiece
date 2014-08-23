using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlColumnAttribute : Attribute
    {
        public SqlColumnAttribute()
        {

        }

        public SqlColumnAttribute(string name)
            : this()
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
