using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class FileTypeExtensions
    {
        public static string GetExtensionType(this string contentType)
        {
            var type = string.Empty;
            switch (contentType)
            {
                case "image/jpeg":
                case "image/pjpeg":
                    type = ".jpg";
                    break;
                case "image/gif":
                    type = ".gif";
                    break;
                case "image/png":
                case "image/x-png":
                    type = ".png";
                    break;
                case "image/x-ms-bmp":
                    type = ".bmp";
                    break;
                case "text/plain":
                case "text/richtext":
                case "text/html":
                    type = ".txt";
                    break;
                case "application/zip":
                case "application/x-zip-compressed":
                    type = ".zip";
                    break;
                case "application/x-rar-compressed":
                    type = ".rar";
                    break;
                default:
                    break;
            }
            return type;
        }
    }
}
