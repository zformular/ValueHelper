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

namespace ValueHelper.Exception
{
    public class FileExsistException : System.Exception
    {
        private String message = "文件已存在!";

        public FileExsistException() { }

        public FileExsistException(String exception)
        {
            this.message = exception;
        }

        public new String Message
        {
            get { return message; }
        }
    }
}
