/*
  >>>------ Copyright (c) 2012 zformular ----> 
 |                                            |
 |            Author: zformular               |
 |        E-mail: zformular@163.com           |
 |             Date: 10.4.2012                |
 |                                            |
 ╰==========================================╯
 
*/

using System;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Reflection;

namespace ValueHelper.FileHelper.OfficeHelper
{
    public partial class WordHelper : FileBase.FileBase
    {
        public WordHelper(String fileName)
        {
            FileFullName = fileName;
            FileName = Path.GetFileName(fileName);
            FileExtension = Path.GetExtension(fileName);
            DirectoryPath = Path.GetDirectoryName(fileName);
        }

        public override bool CreateFile()
        {
            if (CheckParams())
            {
                return CreateFile(FileFullName);
            }
            else
                return false;

        }

        public override bool Write(string context)
        {
            return Write(context, false);
        }

        public override bool Write(string context, bool append)
        {
            return writeContext(context, append);
        }

        public override bool WriteLine(string context)
        {
            return WriteLine(context, false);
        }

        public override bool WriteLine(string context, bool append)
        {
            return writeContext(context, append);
        }

        private Boolean writeContext(string context, bool append)
        {
            try
            {
                if (File.Exists(FileFullName))
                {
                    Object fileName = (Object)FileFullName;
                    Application wordApp = new Application();
                    Document wordDoc = wordApp.Documents.Open(ref fileName, ref missingValue, ref missingValue, ref missingValue
                    , ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue
                    , ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue);
                    if (!append)
                        wordDoc.Content.Text = "";
                    wordDoc.Paragraphs.Last.Range.Text += context;
                    wordDoc.Save();
                    wordDoc.Close(ref missingValue, ref missingValue, ref missingValue);
                    wordApp.Quit(ref missingValue, ref missingValue, ref missingValue);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public override string ReadContext()
        {
            return ReadContext((Object)FileFullName);
        }
    }

    public partial class WordHelper
    {
        private static Object missingValue = Missing.Value;
        private static Object formate = WdSaveFormat.wdFormatDocument;

        public static Boolean CreateFile(Object fileName)
        {
            if (!File.Exists(fileName.ToString()))
            {
                Application wordApp = new Application();
                wordApp.Visible = false;
                Document wordDoc = wordApp.Documents.Add(ref missingValue, ref missingValue, ref missingValue, ref missingValue);
                wordDoc.SaveAs(ref fileName, ref formate, ref missingValue, ref missingValue, ref missingValue, ref missingValue
                    , ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue
                    , ref missingValue, ref missingValue, ref missingValue, ref missingValue);
                wordDoc.Close(ref missingValue, ref missingValue, ref missingValue);
                wordApp.Quit(ref missingValue, ref missingValue, ref missingValue);
                return true;
            }
            return false;
        }

        public static String ReadContext(Object fileName)
        {
            if (File.Exists(fileName.ToString()))
            {
                Application wordApp = new Application();
                wordApp.Visible = false;
                Document wordDoc = wordApp.Documents.Open(ref fileName, ref missingValue, ref missingValue, ref missingValue
                    , ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue
                    , ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue);
                String result = wordDoc.Content.Text;
                result = System.Text.RegularExpressions.Regex.Replace(result, @"\r",
                    new System.Text.RegularExpressions.MatchEvaluator(delegate(System.Text.RegularExpressions.Match match)
                {
                    return "\r\n";
                }), System.Text.RegularExpressions.RegexOptions.Compiled);
                wordDoc.Close(ref missingValue, ref missingValue, ref missingValue);
                wordApp.Quit(ref missingValue, ref missingValue, ref missingValue);
                return result;
            }
            return "";
        }
    }
}
