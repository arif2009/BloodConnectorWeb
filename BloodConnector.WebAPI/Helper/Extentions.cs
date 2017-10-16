using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BloodConnector.WebAPI.Helper
{
    public static class Extentions
    {
        public static IHtmlString WeComment(this HtmlHelper html, string comment)
        {
            #if DEBUG
            // Fix for jQuery / JSON problem:
            //comment = comment.Replace("[", "¤-- ").Replace("]", " --¤");
            return html.Raw("<!-- " + comment + " -->");
            #else
            return html.Raw(string.Empty);
            #endif
        }

        public static byte[] ToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ByteToString(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        public static string Encrypt(this string text)
        {
            return Convert.ToBase64String(text.ToByteArray());
        }

        public static string Decrypt(this string text)
        {
            return Convert.FromBase64String(text).ByteToString();
        }
    }
}