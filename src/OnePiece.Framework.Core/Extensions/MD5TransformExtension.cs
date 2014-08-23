using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class MD5TransformExtension
    {
        public static MD5 Append(this MD5 md5, byte[] bytes)
        {
            return Transform(md5, bytes);
        }

        public static MD5 Append(this MD5 md5, string rawText)
        {
            return Transform(md5, Encoding.UTF8.GetBytes(rawText));
        }

        internal static MD5 Transform(this MD5 md5, byte[] bytes)
        {
            if (md5 == null)
                md5 = new MD5CryptoServiceProvider();

            md5.TransformBlock(bytes, 0, bytes.Length, null, 0);

            return md5;
        }
    }
}
