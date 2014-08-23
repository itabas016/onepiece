using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SubSonicDatabaseAttribute : Attribute
    {
        public SubSonicDatabaseAttribute(string database)
        {
            this.Database = database;
        }

        public string Database { get; set; }
    }
}
