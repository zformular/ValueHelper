﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ValueHelper.Infrastructure;
using ValueHelper.EncryptHelper;

namespace ValueHelper.test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ValueFileHelper

            //var baseUrl = AppDomain.CurrentDomain.BaseDirectory;
            //var filefullName = Path.Combine(baseUrl, "test.txt");
            //IValueFile fileHelper = new FileHelper(filefullName);

            //try
            //{
            //    // 写入内容
            //    fileHelper.WriteContent("test");

            //    // 读取内容
            //    var content = fileHelper.ReadContent();
            //    Console.WriteLine(content);
            //}
            //catch (System.Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Console.WriteLine("FileHelper Result");

            #endregion

            var source = "hello world!";
            #region ValueMD5Helper

            var encryptCodeMD5 = MD5Helper.Encrypt(source);
            Console.WriteLine(encryptCodeMD5);
            Console.WriteLine("MD5Helper Result");

            #endregion

            #region ValueDESHelper

            // 由于每次key 都不同所以要注意加密与解密的时间性
            // DESkey是64位二进制,转字符串的话就是8个字符
            var key = DESHelper.GenerateKey();

            var encryptCodeDES = DESHelper.Encrypt(source, key);
            Console.WriteLine(encryptCodeDES);

            var decryptCodeDES = DESHelper.Decrypt(source, key);
            Console.WriteLine(decryptCodeDES);
            Console.WriteLine("DESHelper Result");

            #endregion

            #region StringHelper

            String[] array = new String[] { "1", "2", "3", "4" };
            var str = StringHelper.ConvertToString(array, ';');
            Console.WriteLine(str);

            var strArry = StringHelper.Split(str, ';');
            foreach (var item in strArry)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("StringHelper Result");
            #endregion

            #region RandomHelper

            Console.WriteLine(RandomHelper.NewRandom());

            Console.WriteLine(RandomHelper.NewRandom(6));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.Number));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.String));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.Number, 6));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.String, 6));

            Console.WriteLine(RandomHelper.NewRandom('Z', 'Z'));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.Number, '1', 'a'));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.String, 'a', 'f'));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.Number, '1', '4', 6));

            Console.WriteLine(RandomHelper.NewRandom(RandomType.String, 'a', 'f', 6));

            Console.WriteLine("RandomHelper Result");
            #endregion


        }
    }
}
