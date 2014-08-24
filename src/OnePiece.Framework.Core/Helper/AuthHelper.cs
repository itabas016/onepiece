using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class AuthHelper
    {
        /// <summary>
        /// 获取验证码【字符串】
        /// </summary>
        /// <param name="Length">验证码长度【必须大于0】</param>
        /// <returns></returns>
        public static string VerificationText(int Length)
        {
            char[] _verification = new Char[Length];
            Random _random = new Random();
            char[] _dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < Length; i++)
            {
                _verification[i] = _dictionary[_random.Next(_dictionary.Length - 1)];
            }
            return new string(_verification);
        }
    }
}
