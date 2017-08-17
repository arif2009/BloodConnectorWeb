using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}