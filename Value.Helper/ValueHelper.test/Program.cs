using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValueHelper.EncryptHelper;
using ValueHelper.Infrastructure;
using ValueHelper.MIMEHelper;
using ValueHelper.TDCodeHelper.QR2DCodeHelper;
using ValueHelper.TDCodeHelper.QR2DCodeHelper.Infrastructure;
using System.Windows.Forms;
//using ValueHelper.OtherHelper;

namespace ValueHelper.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            #region ValueFileHelper

            //FileBase fileHelper = FileBase.GetFileBase("E:\\Text2.txt");
            //fileHelper.CreateFile();
            ////fileHelper.WriteLine("asdsadasdsadsadaanak\r\ndasdasd", true);
            //String context = fileHelper.ReadContext();
            //Console.WriteLine(context);

            #endregion

            var source = "123456";
            #region ValueMD5Helper

            var encryptCodeMD5 = MD5Helper.Encrypt(source);
            Console.WriteLine(encryptCodeMD5);
            Console.WriteLine("MD5Helper Result");
            Console.WriteLine();
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
            Console.WriteLine();
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

            var str2 = "asdajdahda\r\nasdasda\r\nasdasdasd\r\n";
            Console.WriteLine("asdajdahda\\r\\nasdasda\\r\\nasdasdasd\\r\\nsdfsdfsdf");
            Console.WriteLine("StringHelper.SplitByCRLF()");
            var tst = StringHelper.SplitByCRLF(str2, StringSplitOptions.RemoveEmptyEntries);

            var strArry2 = StringHelper.SplitByCRLF(str2, StringSplitOptions.None);
            foreach (var item in strArry2)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
            Console.WriteLine("StringHelper Result");
            Console.WriteLine();
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
            Console.WriteLine();

            #endregion

            #region QPHelper

            String QPString = "管理员";
            String QPEncodeStr = QPHelper.Encrypt(QPString, Encoding.UTF8);
            Console.WriteLine(QPEncodeStr);
            Console.WriteLine(QPHelper.Decrypt2(QPEncodeStr, Encoding.UTF8));
            Console.WriteLine(QPHelper.Decrypt(QPEncodeStr, Encoding.UTF8));
            #endregion

            #region MIMEHelper

            var mime = "\r\nContent-Type: multipart/alternative; \r\n\tboundary=\"----=_Part_65603903_1101983096.1350874042576\"\r\nDate: Mon, 22 Oct 2012 10:47:23 +0800 (CST)\r\nFrom: =?UTF-8?B?572R5piT6YKu5Lu25Lit5b+D?= <phone@service.netease.com>\r\nSubject: =?UTF-8?B?572R5piT6YKu566x5L2/55So5pWw5o2u6LSm5Y2V?=\r\n =?UTF-8?B?IO+8iDIwMTLlubQ55pyI77yJ?=\r\n";
            ValueMIME.SerializeMIME(mime);

            #endregion

            #region TDCodeHelper



            #endregion

            //Byte asd = (Byte)'s';
            //var asdddd = asd | 0;

            //var asdd = Convert.ToString(asd, 2);
            //Value2DCode value2DCode = new Value2DCode();

            //var bitmap = value2DCode.ProduceBitmap("123");
            //bitmap.Save("D:\\二维码测试.jpg");


            #region ValueWebcam


            //Form frmtest = new Form();
            //frmtest.Width = 400;
            //frmtest.Height = 400;
            //frmtest.Controls.Add(valueWebcam.Content);
            //valueWebcam.OpenWebcam();

            //frmtest.FormClosing += new FormClosingEventHandler(frmtest_FormClosing);


            #endregion

            /// 未实现
            //////////#region JSONHelper

            //////////var jsonstr = "[{\"UserId\":\"11\",\"UserName\":{\"FirstName\":\"323\",\"LastName\":\"2323\"},\"Keys\":[\"xiaoming\",\"xiaohong\"]},{\"UserId\":\"22\",\"UserName\":{\"FirstName\":\"323\",\"LastName\":\"2323\"},\"Keys\":[\"xiaoming\",\"xiaohong\"]},{\"UserId\":\"33\",\"UserName\":{\"FirstName\":\"323\",\"LastName\":\"2323\"},\"Keys\":[\"xiaoming\",\"xiaohong\"]}]";
            //////////JSONHelper.Parse(jsonstr);

            //////////#endregion

            Console.ReadLine();
        }

        static ValueWebcam.ValueWebcam valueWebcam = new ValueWebcam.ValueWebcam(0, 0, 200, 200);
        static void frmtest_FormClosing(object sender, FormClosingEventArgs e)
        {
            valueWebcam.CloseWebcam();
        }
    }
}
