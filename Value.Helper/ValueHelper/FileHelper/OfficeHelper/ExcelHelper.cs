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
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace ValueHelper.FileHelper.OfficeHelper
{
    public partial class ExcelHelper : FileBase.FileBase
    {
        public ExcelHelper(String fileName)
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
            return false;
        }

        public override string ReadContext()
        {
            Application excelApp = new Application();
            excelApp.Visible = false;
            Workbook excelBook = excelApp.Workbooks.Open(FileFullName, missingValue, missingValue, missingValue, missingValue,
                missingValue, missingValue, missingValue, missingValue, missingValue, missingValue, missingValue, missingValue,
                missingValue, missingValue);
            String result = String.Empty;
            Int32 sheetIndex = 1;
            foreach (Worksheet excelSheet in excelBook.Sheets)
            {
                result += "Sheet" + sheetIndex + ":\r\n";
                Range rang = excelSheet.get_Range("A1", "Z24");
                var value = ((Array)rang.Cells.Value2);
                for (int rowIndex = 1; rowIndex < 25; rowIndex++)
                {
                    String rowValue = String.Empty;
                    for (int index = 1; index < 25; index++)
                    {
                        if (value.GetValue(rowIndex, index) != null)
                        {
                            rowValue += value.GetValue(rowIndex, index);
                            rowValue += " ";
                        }
                    }
                    if (rowValue != String.Empty)
                    {
                        result += rowValue;
                        result += "\r\n";
                    }
                }
                sheetIndex++;
            }
            excelBook.Close(missingValue, missingValue, missingValue);
            excelApp.Quit();

            return result;
        }
    }

    public partial class ExcelHelper
    {
        public static Object missingValue = Missing.Value;

        public static Boolean CreateFile(Object fileName)
        {
            try
            {
                if (!File.Exists(fileName.ToString()))
                {
                    Application excelApp = new Application();
                    excelApp.Visible = false;
                    Workbook excelBook = excelApp.Workbooks.Add(missingValue);
                    excelBook.SaveAs(fileName, missingValue, missingValue, missingValue, missingValue, missingValue, XlSaveAsAccessMode.xlNoChange
                        , missingValue, missingValue, missingValue, missingValue, missingValue);
                    excelBook.Close(missingValue, missingValue, missingValue);
                    excelApp.Quit();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static String ReadContext(Object fileName)
        {
            return null;
        }
    }
}
