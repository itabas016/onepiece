using OnePiece.Framework.Core.Data;
using OnePiece.Framework.SubSonic;
using SubSonic.SqlGeneration.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Tests.Core.SubSonic
{
    [Serializable]
    [SubSonicDatabase("testdb")]
    [SubSonicTableNameOverride("test")]
    public class SubSonicModelTest : EntityBase
    {
        [SubSonicStringLength(64)]
        public string Name { get; set; }

        [SubSonicIndex]
        [Display(Name = "用户ID"), SqlColumn]
        public int UserId { get; set; }
    }
}
