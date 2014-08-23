using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core.Security
{
    public interface IUrlSignature
    {
        bool IsValid();

        string Sign(SignatureContext context);
    }
}
