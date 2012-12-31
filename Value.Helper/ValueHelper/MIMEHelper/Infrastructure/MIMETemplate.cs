using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValueHelper.MIMEHelper.Infrastructure
{
    public class MIMETemplate
    {
        public const String From = @"\r\nFrom:\s+(?<from>=\?[^?]*\?[B|Q]\?[^?]*\?=\s*.*)\r\n";
        public const String Date = @"\r\nDate:\s+(?<date>.*)\r\n";
        public const String Subject = @"\r\nSubject:\s+(?<g1>=\?[^?]*\?[B|Q]\?[^?]*\?=)(?(\r\n )\r\n (?<g2>=\?[^?]*\?[B|Q]\?[^?]*\?=))\r\n";
        public const String ContentEncoding = @"\r\nContent-Transfer-Encoding:\s+(?<encoding>.*)\r\n";
        public const String EncodeTemplate = @"=\?(?<charset>[^?]*)\?(?<entype>[B|Q])\?(?<data>[^?]*)\?=";

        //public const String ContentType = @"\r\nContent-Type:\s+(?<type>[^;]*);\s*" +
        //    @"(\r\n[\t|\s]*)?(?(charse)charset=\s*(?("")""(?<charset>[^""]*)""|(?<charset>.*)))" +
        //    @"(\r\n[\t|\s]*)?(?(boundar)boundary=\s*(?("")""(?<boundary>[^""]*)""|(?<boundary>.*)))" +
        //    @"(\r\n[\t|\s]*)?(?(nam)name=\s*(?("")""(?<name>[^""]*)""|(?<name>.*)))" +
        //    "\r\n";

        public const String ContentType = @"\r\nContent-Type:\s+(?<type>[^;]*);\s*" +
            @"(\r\n[\t|\s]*)?(?(charse)charset=\s*(?("")""(?<charset>[^""]*)""|(?<charset>.*)))(;)?" +
            @"(\r\n[\t|\s]*)?(?(boundar)boundary=\s*(?("")""(?<boundary>[^""]*)""|(?<boundary>.*)))(;)?" +
            @"(\r\n[\t|\s]*)?(?(nam)name=\s*(?("")""(?<name>[^""]*)""|(?<name>.*)))(;)?" +
            "\r\n";

        public const String Context = @"\r\n\r\n(?<context>[\s\S]*)\r\n";

        public static String PartialBody(String boundary)
        {
            var template = String.Concat(boundary, @"(?<data>\r\n[\s\S]*\r\n)", boundary, "--");
            return template;
        }
    }
}
