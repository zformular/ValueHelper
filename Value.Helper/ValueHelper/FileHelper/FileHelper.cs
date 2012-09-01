/*
  >>>------ Copyright (c) 2012 zformular ----> 
 |                                            |
 |            Author: zformular               |
 |        E-mail: zformular@163.com           |
 |             Date: 8.29.2012                |
 |                                            |
 |                                            |
 ╰==========================================╯
*/

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using ValueHelper.Exception;
using ValueHelper.Infrastructure;
using System.Text;

namespace ValueHelper
{
    public class FileHelper : IValueFile
    {
        private String newLine = Environment.NewLine;

        private String fileFullName;
        private String fileName;
        private String content;

        public FileHelper() { }

        public FileHelper(String fileFullName)
        {
            this.fileFullName = fileFullName;
            this.fileName = Path.GetFileName(this.fileFullName);
        }

        public FileHelper(String fileFullName, String fileName)
        {
            this.fileFullName = fileFullName;
            this.fileName = fileName;
        }

        #region IValueFile 成员

        /// <summary>
        ///  读写文件全名
        /// </summary>
        public String FileFullName
        {
            get { return fileFullName; }
            set { fileFullName = value; }
        }

        /// <summary>
        ///  读写文件名
        /// </summary>
        public String FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        ///  读写文件内容
        /// </summary>
        public String Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        ///  创建文件
        /// </summary>
        /// <param name="fileFullName"></param>
        public void CreateFile(String fileFullName)
        {
            if (File.Exists(fileFullName))
                throw new FileExsistException();

            try
            {
                Monitor.Enter(this);
                var filePath = this.getFilePath(fileFullName);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                FileStream fileStream = new FileStream(fileFullName, FileMode.Create);
                fileStream.Flush();
                fileStream.Close();
                Monitor.Exit(this);
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        ///  判断文本是否存在
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Boolean ContentExsist(String content)
        {
            if (String.IsNullOrEmpty(fileFullName))
                throw new ArgumentNullException("FileFullName", "文件名不能为空");

            if (String.IsNullOrEmpty(content))
                throw new ArgumentNullException("Content", "文件内容不能为空!");
            try
            {
                var texts = File.ReadAllLines(fileFullName, Encoding.UTF8);
                var result = false;
                for (int index = 0; index < texts.Length; index++)
                {
                    if (texts[index].Contains(content))
                        result = true;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  写入内容
        /// </summary>
        public void WriteContent()
        {
            this.writeContent();
        }

        /// <summary>
        ///  写入内容
        /// </summary>
        /// <param name="content"></param>
        public void WriteContent(String content)
        {
            this.content = content;
            this.writeContent();
        }

        /// <summary>
        ///  读取内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public String ReadContent(String content)
        {
            if (String.IsNullOrEmpty(fileFullName))
                throw new ArgumentNullException("FileFullName", "文件名不能为空");

            if (String.IsNullOrEmpty(content))
                throw new ArgumentNullException("Content", "文件内容不能为空!");

            var texts = File.ReadAllLines(fileFullName, Encoding.UTF8);
            var result = String.Empty;
            for (int index = 0; index < texts.Length; index++)
            {
                if (texts[index].Contains(content))
                    result += (texts[index] + ";");
            }
            return result;
        }

        /// <summary>
        ///  读取内容
        /// </summary>
        /// <returns></returns>
        public String ReadContent()
        {
            if (String.IsNullOrEmpty(fileFullName))
                throw new ArgumentNullException("FileFullName", "文件名不能为空");

            StreamReader streamReader = new StreamReader(fileFullName);

            var result = streamReader.ReadToEnd();
            return result;
        }

        #endregion

        /// <summary>
        ///  写入内容
        /// </summary>
        private void writeContent()
        {
            if (String.IsNullOrEmpty(fileFullName))
                throw new ArgumentNullException("FileFullName", "文件名不能为空");

            if (content == null)
                throw new ArgumentNullException("Content", "文件内容不能为空!");

            try
            {
                Monitor.Enter(this);
                if (!File.Exists(fileFullName))
                    throw new FileNotFoundException();

                StreamWriter streamWriter = new StreamWriter(fileFullName, true);
                streamWriter.WriteLine(content);
                streamWriter.Flush();
                streamWriter.Close();
                Monitor.Exit(this);
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        ///  获得文件名
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        private String getFileName(String fileFullName)
        {
            try
            {
                var result = Path.GetFileName(fileFullName);
                return result;
            }
            catch (ArgumentException ex)
            {
                throw (ex);
            }
        }

        private String getFilePath(String fileFulName)
        {
            var fileName = Path.GetFileName(fileFulName);
            var filePath = fileFulName.Substring(0, fileFulName.Length - fileName.Length - 1);
            return filePath;
        }

        /// <summary>
        ///  获得扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private String getExtensionName(String fileName)
        {
            try
            {
                var result = Path.GetExtension(fileName);
                return result;
            }
            catch (ArgumentException ex)
            {
                throw (ex);
            }
        }
    }
}
