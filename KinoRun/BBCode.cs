using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoRun
{
    class BBCode
    {
        private static string src = "";
        private static Dictionary<string, string> BBCodePars = new Dictionary<string, string>();

        private static void prepareTranslit()
        {
            BBCodePars.Add("[b]", "<b>");
            BBCodePars.Add("[/b]", "</b>");
            BBCodePars.Add("[i]", "<span style=\"font-style:italic;\">");
            BBCodePars.Add("[/i]", "");
            BBCodePars.Add("[u]", "<span style=\"text-decoration:underline;\">");
            BBCodePars.Add("[/u]", "</span>");
            BBCodePars.Add("[code]", "<pre class=\"prettyprint\">");
            BBCodePars.Add("[/code]", "</pre>");
            BBCodePars.Add("[img]" + src + "[/img]", "<img src=\"" + src + "\" />");
            BBCodePars.Add("[quote]", "<blockquote>");
            BBCodePars.Add("[/quote]", "</blockquote>");
            //BBCodePars.Add("[list]", "<ul>");
            //BBCodePars.Add("[*]", "<li>");
            BBCodePars.Add("[url]", "<a href=\"");
            BBCodePars.Add("[/url]", "\">");
            BBCodePars.Add("[url=" + src + "]", "<a href=\"" + src + "\" />");
        }

        public static string BBCodeToHtml(string BBCodeTag, string sourceText)
        {
            string ans = "";

            src = sourceText;
            if (BBCodePars.Count < 10)
                prepareTranslit();

            ans = BBCodePars[BBCodeTag + src];

            return ans;
        }
    }
}