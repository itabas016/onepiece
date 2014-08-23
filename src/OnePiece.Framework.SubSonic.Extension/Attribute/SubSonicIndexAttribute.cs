using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SubSonicIndexAttribute : Attribute
    {
        /// <summary>
        /// index name,
        /// if you do not specify this property, system will generate on default name for it. 
        /// Default format is "ix_tablename_columnname"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// default schema is dbo. (empty)
        /// </summary>
        public string Schema { get; set; }

        #region ctor
        public SubSonicIndexAttribute()
        {

        }

        public SubSonicIndexAttribute(string name)
            : this()
        {
            this.Name = name;
        }

        public SubSonicIndexAttribute(string name, string schema)
            : this(name)
        {
            this.Schema = schema;
        }
        #endregion
    }
}
