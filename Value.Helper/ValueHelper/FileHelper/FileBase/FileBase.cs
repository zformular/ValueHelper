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
using ValueHelper.Infrastructure;
using System.IO;
using ValueHelper.FileHelper.OfficeHelper;

namespace ValueHelper.FileHelper.FileBase
{
    public class FileBase : IValueFile
    {
        protected String FileFullName = null;
        protected String FileName = null;
        protected String DirectoryPath = null;
        protected String FileExtension = null;

        private static FileBase fileBase;

        public static FileBase GetFileBase(String fileName)
        {
            String fileExtension = Path.GetExtension(fileName).ToLower();
            switch (fileExtension)
            {
                case ".txt":
                    fileBase = new FileHelper(fileName);
                    break;
                case ".doc":
                    fileBase = new WordHelper(fileName);
                    break;
                case ".xls":
                    fileBase = new ExcelHelper(fileName);
                    break;
                default:
                    fileBase = new FileHelper(fileName);
                    break;
            }
            return fileBase;
        }

        public Boolean CheckParams()
        {
            if (String.IsNullOrEmpty(FileFullName))
                return false;
            if (String.IsNullOrEmpty(FileName))
                return false;
            if (String.IsNullOrEmpty(DirectoryPath))
                return false;

            return true;
        }

        #region IValueFile 成员

        public virtual Boolean CreateFile()
        {
            throw new NotImplementedException();
        }

        public virtual Boolean Write(String context)
        {
            throw new NotImplementedException();
        }

        public virtual Boolean Write(string context, Boolean append)
        {
            throw new NotImplementedException();
        }

        public virtual Boolean WriteLine(String context)
        {
            throw new NotImplementedException();
        }

        public virtual Boolean WriteLine(string context, Boolean append)
        {
            throw new NotImplementedException();
        }

        public virtual String ReadContext()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable 成员

        private Boolean disposed = false;
        protected void Dispose(Boolean disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    FileFullName = null;
                    FileName = null;
                    DirectoryPath = null;
                    FileExtension = null;
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region 析构函数

        ~FileBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
