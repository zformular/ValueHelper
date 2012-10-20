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
using ValueHelper.FileHelper.OfficeHelper;
using System.Text;

namespace ValueHelper.FileHelper
{
    public sealed partial class FileHelper : FileBase.FileBase
    {
        public FileHelper(String fileName)
        {
            FileFullName = fileName;
            FileName = Path.GetFileName(fileName);
            FileExtension = Path.GetExtension(fileName);
            DirectoryPath = Path.GetDirectoryName(fileName);
        }

        public override Boolean CreateFile()
        {

            if (CheckParams())
            {
                return CreateFile(FileFullName);
            }
            else
                return false;
        }

        public override Boolean Write(String context)
        {
            return Write(context, false);
        }

        public override Boolean Write(String context, Boolean append)
        {
            try
            {
                if (File.Exists(FileFullName))
                {
                    StreamWriter streamWriter = new StreamWriter(FileFullName, append);
                    streamWriter.Write(context);
                    streamWriter.Flush();
                    streamWriter.Close();
                    streamWriter.Dispose();
                    streamWriter = null;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public override Boolean WriteLine(String context)
        {
            return WriteLine(context, false);
        }

        public override Boolean WriteLine(String context, Boolean append)
        {
            try
            {
                if (File.Exists(FileFullName))
                {
                    StreamWriter streamWriter = new StreamWriter(FileFullName, append);
                    streamWriter.WriteLine(context);
                    streamWriter.Flush();
                    streamWriter.Close();
                    streamWriter.Dispose();
                    streamWriter = null;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public override String ReadContext()
        {
            try
            {
                if (CheckParams())
                {
                    return ReadContext(FileFullName);
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }

    public partial class FileHelper
    {
        /// <summary>
        ///  创建文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>成功返回true,失败返回false</returns>
        public static Boolean CreateFile(String fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    return false;
                else
                {
                    String directoryPath = Path.GetDirectoryName(fileName);
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    FileStream fileStream = new FileStream(fileName, FileMode.CreateNew);
                    fileStream.Close();
                    fileStream.Dispose();
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        ///  读取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static String ReadContext(String fileName)
        {
            StreamReader streamReader = new StreamReader(fileName, Encoding.Default);
            String result = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            streamReader = null;
            return result;
        }
    }
}
