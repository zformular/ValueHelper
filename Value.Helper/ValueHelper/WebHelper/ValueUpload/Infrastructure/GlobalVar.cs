using System;

namespace WebHelper.ValueUpload.Infrastructure
{
    public class GlobalVar
    {
        /// <summary>
        ///  千字节
        /// </summary>
        public const Int32 Kilobyte = 1024;

        /// <summary>
        ///  换行符
        /// </summary>
        public static String newline = Environment.NewLine;

        /// <summary>
        ///  获得Http报文中分割的边界字符串
        /// </summary>
        public const String BoundaryPattern = @"\s+boundary=(?("")""(?<boundary>.*)""|(?<boundary>.*))";

        /// <summary>
        ///  分割的Http报文的头
        ///  实例:
        ///    Content-Disposition: form-data; name="uploadfile"; filename="Plan.txt"
        ///    Content-Type: text/plain
        /// </summary>
        public static String FileHeaderPattern(String inputName)
        {
            String pattern = @"Content-Disposition:\s+form-data;\s+name=(?("")""" + inputName + @"""|" + inputName + @");" +
              @"\s+filename=(?("")""(?<filename>.*\..*)""|(?<filename>.*\..*))\r\nContent-Type:\s+.*";
            return pattern;
        }

        /// <summary>
        ///  文件头
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        public static String ContentDispostionTemplate(String inputName)
        {
            return "Content-Disposition: form-data; name=\"" + inputName + "\"; filename=\"";
        }

        public const String ParamTmeplate = @"Content-Disposition: form-data; name=(?("")""(?<name>.*)""|(?<name>.*))\r\n\r\n(?<value>(.*(\r\n)?)*)\r\n";
    }
}
