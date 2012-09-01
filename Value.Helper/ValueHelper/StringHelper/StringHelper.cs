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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValueHelper
{
    public class StringHelper
    {
        public static String ConvertToString(String[] sources, Char separator)
        {
            var result = String.Empty;
            foreach (var item in sources)
                result += (item + separator);
            return result;
        }

        public static String[] Split(String source, Char separator)
        {
            var items = source.Split(separator);
            var itemCount = 0;
            if (source.EndsWith(separator.ToString()))
                itemCount = items.Length - 1;
            else
                itemCount = items.Length;

            String[] result = new String[itemCount];
            Array.Copy(items, result, result.Length);
            return result;
        }
    }
}
