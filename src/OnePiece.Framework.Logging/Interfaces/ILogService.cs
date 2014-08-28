using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Logging
{
    public interface ILogService
    {
        void Error(string conent);
        void Info(string content);
        void Debug(string content);
        void SVC(string content);
    }
}
