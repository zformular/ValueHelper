/*
  >>>------ Copyright (c) 2012 zformular ----> 
 |                                            |
 |            Author: zformular               |
 |        E-mail: zformular@163.com           |
 |             Date: 10.7.2012                |
 |                                            |
 ╰==========================================╯
 
*/

using System;
using System.Text;
using System.Web;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using WebHelper.ValueUpload.UploadEvents;

namespace WebHelper.ValueUpload.Infrastructure
{
    public class UploadBase
    {
        protected HttpWorkerRequest httpWorkerRequest;
        protected HttpContextBase httpContextBase;
        protected Encoding encoding;
        protected Double totalLength;

        private UploadInfo uploadInfo = new UploadInfo();
        private String filePath;
        private Double received = 0;
        private FileStream fileStream;
        private String fileNamePattern;
        private Byte[] unSortBuffer = null;
        private Byte[] packetBuffer = null;


        private Byte[] boundaryBytes;
        private Byte[] headStartBytes;
        private Byte[] doubleNewlineBytes;
        private Byte[] boundaryBreakBytes;
        private Byte[] packetEndBytes;

        protected event UpdateDelegate OnUpdate;
        protected UpdateEventArgs updateEventArgs = new UpdateEventArgs();

        protected UploadInfo Save(String inputName, String filePath)
        {
            try
            {
                this.filePath = filePath;
                this.uploadProcess(inputName, ProcessType.Save);
                uploadInfo.Success = true;
            }
            catch (IOException)
            {
                uploadInfo.Success = false;
                uploadInfo.Exception = new Exception("已存在同名文件");
            }
            catch (Exception ex)
            {
                uploadInfo.Success = false;
                uploadInfo.Exception = ex;
            }
            return uploadInfo;
        }

        protected UploadInfo SaveAs(String inputName, String fileName)
        {
            try
            {
                this.filePath = fileName;
                this.uploadProcess(inputName, ProcessType.SaveAs);
                uploadInfo.Success = true;
            }
            catch (IOException)
            {
                uploadInfo.Success = false;
                uploadInfo.Exception = new Exception("已存在同名文件");
            }
            catch (Exception ex)
            {
                uploadInfo.Success = false;
                uploadInfo.Exception = ex;
            }
            return uploadInfo;
        }

        /// <summary>
        ///  循环处理读取的数据包
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="processType"></param>
        private void uploadProcess(String inputName, ProcessType processType)
        {
            String contentType = httpWorkerRequest.GetKnownRequestHeader(HttpWorkerRequest.HeaderContentType);
            String boundary = "--" + Regex.Match(contentType, GlobalVar.BoundaryPattern).Groups["boundary"].Value;
            this.setBoundary(boundary, inputName);

            Int32 preloaded = httpWorkerRequest.GetPreloadedEntityBodyLength();
            this.totalLength = httpWorkerRequest.GetTotalEntityBodyLength();

            Byte[] body;
            if (preloaded > 0)
            {
                body = httpWorkerRequest.GetPreloadedEntityBody();
            }
            else
            {
                body = new Byte[GlobalVar.Kilobyte];
                preloaded = httpWorkerRequest.ReadEntityBody(body, body.Length);
            }
            received += preloaded;
            this.WriteToDisk(body, preloaded, processType);
            Int32 loaded = preloaded;

            // 该类负责欺骗ASP.NET 让其认为没有文件上传就不会尝试缓存文件,不会报文件太大的错
            StaticWorkerRequest staticWorkerRequest = new StaticWorkerRequest(httpWorkerRequest, httpWorkerRequest.GetPreloadedEntityBody());
            FieldInfo field = HttpContext.Current.Request.GetType().GetField("_wr", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(HttpContext.Current.Request, staticWorkerRequest);
            if (!httpWorkerRequest.IsEntireEntityBodyIsPreloaded())
            {
                Byte[] buffer = new Byte[GlobalVar.Kilobyte];
                while (this.totalLength - received >= loaded && httpWorkerRequest.IsClientConnected())
                {
                    loaded = httpWorkerRequest.ReadEntityBody(buffer, buffer.Length);
                    this.WriteToDisk(buffer, loaded, processType);
                    received += loaded;
                }
                Int32 remaining = (Int32)(totalLength - received);
                buffer = new Byte[remaining];
                loaded = httpWorkerRequest.ReadEntityBody(buffer, buffer.Length);
                this.WriteToDisk(buffer, loaded, processType);
            }
            // 防止最后次轮询 保存了数据包表单内容但没有分析
            analysePacket();
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
        }

        /// <summary>
        ///  严郑声明 这里的东西不能改!
        /// </summary>
        /// <param name="boundary"></param>
        /// <param name="inputName"></param>
        private void setBoundary(String boundary, String inputName)
        {
            this.boundaryBytes = encoding.GetBytes(String.Concat(boundary, GlobalVar.newline));
            this.boundaryBreakBytes = encoding.GetBytes(String.Concat(GlobalVar.newline, boundary, GlobalVar.newline));
            this.doubleNewlineBytes = encoding.GetBytes(String.Concat(GlobalVar.newline, GlobalVar.newline));
            this.headStartBytes = encoding.GetBytes(String.Concat(boundary, GlobalVar.newline, GlobalVar.ContentDispostionTemplate(inputName)));
            this.packetEndBytes = encoding.GetBytes(String.Concat(GlobalVar.newline, boundary, "--"));
            this.fileNamePattern = GlobalVar.FileHeaderPattern(inputName);
        }

        private Boolean fileEnd = false;
        private void WriteToDisk(Byte[] buffer, Int32 loaded, ProcessType processType)
        {
            // 截断为赋值的字节,合并上次循环遗留的字节
            buffer = sortBuffer(buffer, loaded);
            Int32 headStartIndex = getIndexOf(buffer, this.headStartBytes);
            if (headStartIndex != -1)
            {
                // 获取文件信息,确定文件头位置
                Int32 headEndIndex = getIndexOf(buffer, this.doubleNewlineBytes);
                String text = encoding.GetString(buffer, 0, headEndIndex);
                Match match = Regex.Match(text, this.fileNamePattern, RegexOptions.Compiled);
                String headValue = match.Value;
                if (headValue == String.Empty)
                    throw new ArgumentException("未发现上传的文件,请确保文件存在且file标签的名称正确");
                // 创建文件写入数据
                String fileName = setFileName(match.Groups["filename"].ToString(), processType);
                this.fileStream = new FileStream(fileName, FileMode.CreateNew);
                if (getIndexOf(buffer, this.packetEndBytes) != -1)
                    buffer = cutBuffer(buffer, headEndIndex + this.doubleNewlineBytes.Length, buffer.Length);
                else
                {
                    // 如果没有出现数据包结尾字符串, 则有保留的写入并返回等待下次正确判断
                    // 防止文件小到刚好把分割的字符串给截断,则2次循环都不存在分割线
                    buffer = cutBufferLeftEnd(buffer, headEndIndex + this.doubleNewlineBytes.Length, buffer.Length);
                    this.fileStream.Write(buffer, 0, buffer.Length);
                    this.fileStream.Flush();
                    return;
                }
            }

            Int32 packetEndIndex = getIndexOf(buffer, this.packetEndBytes);
            Int32 contextEndIndex = getIndexOf(buffer, this.boundaryBreakBytes);
            if (packetEndIndex != -1)
            {
                if (!fileEnd)
                {
                    // 到达数据包尾,到达未到达文件尾,出现分割线
                    // 一份为2 文件数据和数据包表单数据分别处理
                    if (contextEndIndex != -1)
                    {
                        analysePacket(cutBuffer(buffer, contextEndIndex, packetEndIndex));
                        buffer = cutBuffer(buffer, 0, contextEndIndex);
                    }
                    // 未出现分割线 则只有文件数据
                    else
                    {
                        buffer = cutBuffer(buffer, 0, packetEndIndex);
                    }
                    fileEnd = true;
                }
                else
                {
                    // 已经到达文件为则所有数据未 数据包表单数据
                    storePacketBuffer(buffer, 0, packetEndIndex);
                    analysePacket(this.packetBuffer);
                }
            }
            else
            {
                if (!fileEnd)
                {
                    // 未到达数据包尾,未到达文件尾, 出现分割线
                    if (contextEndIndex != -1)
                    {
                        // 存储数据包数据
                        // 写入文件数据
                        storePacketBuffer(buffer, contextEndIndex, buffer.Length);
                        buffer = cutBuffer(buffer, 0, contextEndIndex);
                        fileEnd = true;
                    }
                    else
                    {
                        //未到达文件尾,未到达分割线,有保留的切割文件数据
                        buffer = cutBufferLeftEnd(buffer, 0, buffer.Length);
                    }
                }
                else
                {
                    // 未到达数据包尾,到达文件尾,存储数据包表单数据
                    storePacketBuffer(buffer, 0, buffer.Length);
                    return;
                }
            }
            if (fileStream == null)
                throw new Exception("上传失败表单设置有误");
            this.fileStream.Write(buffer, 0, buffer.Length);
            // 触发更新进度的事件
            triggerUpdateEvent();
        }

        /// <summary>
        ///  切割数据 保留最后100位字节
        ///  防止一定大小的文件 在预读取时就把分割线给拆了 导致下面分析有误 
        ///  所以保留最后几百字节为未处理数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private Byte[] cutBufferLeftEnd(Byte[] buffer, Int32 startIndex, Int32 endIndex)
        {
            Int32 end = endIndex > 100 ? 100 : 0;
            unSortBuffer = sortBuffer(buffer, endIndex - end, endIndex);
            Int32 count = endIndex - startIndex - end;
            Byte[] newBuffer = new Byte[count];
            Buffer.BlockCopy(buffer, startIndex, newBuffer, 0, count);
            return newBuffer;

        }

        /// <summary>
        ///  切割数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private Byte[] cutBuffer(Byte[] buffer, Int32 startIndex, Int32 endIndex)
        {
            Byte[] newBuffer = new Byte[endIndex - startIndex];
            Buffer.BlockCopy(buffer, startIndex, newBuffer, 0, endIndex - startIndex);
            return newBuffer;
        }

        /// <summary>
        ///  存储未处理数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="loaded"></param>
        /// <returns></returns>
        private Byte[] sortBuffer(Byte[] buffer, Int32 loaded)
        {
            return sortBuffer(buffer, 0, loaded);
        }

        /// <summary>
        ///  存储未处理数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private Byte[] sortBuffer(Byte[] buffer, Int32 startIndex, Int32 endIndex)
        {
            Int32 count = endIndex - startIndex;
            if (count == 0)
                return buffer;

            Byte[] newBuffer;
            if (unSortBuffer != null)
            {
                newBuffer = new Byte[unSortBuffer.Length + count];
                Buffer.BlockCopy(unSortBuffer, 0, newBuffer, 0, unSortBuffer.Length);
                Buffer.BlockCopy(buffer, startIndex, newBuffer, unSortBuffer.Length, count);
                unSortBuffer = null;
            }
            else
            {
                newBuffer = new Byte[count];
                Buffer.BlockCopy(buffer, startIndex, newBuffer, 0, count);
            }
            return newBuffer;
        }

        /// <summary>
        ///  存储数据包表单数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        private void storePacketBuffer(Byte[] buffer, Int32 startIndex, Int32 endIndex)
        {
            Byte[] newBuffer;
            Int32 count = endIndex - startIndex;
            if (packetBuffer != null)
            {
                newBuffer = new Byte[packetBuffer.Length + count];
                Buffer.BlockCopy(packetBuffer, 0, newBuffer, 0, packetBuffer.Length);
                Buffer.BlockCopy(buffer, startIndex, newBuffer, packetBuffer.Length, count);
            }
            else
            {
                newBuffer = new Byte[count];
                Buffer.BlockCopy(buffer, startIndex, newBuffer, 0, count);
            }
            this.packetBuffer = newBuffer;
        }

        /// <summary>
        ///  分析数据包表单数据
        /// </summary>
        private void analysePacket()
        {
            if (this.packetBuffer != null)
            {
                analysePacket(this.packetBuffer);
            }
        }

        /// <summary>
        ///  分析数据包表单数据
        /// </summary>
        /// <param name="buffer"></param>
        private void analysePacket(Byte[] buffer)
        {
            String text = String.Concat(encoding.GetString(buffer), GlobalVar.newline);
            String boundary = encoding.GetString(this.boundaryBytes).Replace("\r\n", "");
            String[] datas = text.Split(new String[] { boundary }, StringSplitOptions.RemoveEmptyEntries);
            uploadInfo.FileLength = this.totalLength;
            foreach (String data in datas)
            {
                Match match = Regex.Match(data, GlobalVar.ParamTmeplate);
                String test = match.Value;
                if (match.Success)
                {
                    uploadInfo.Form[match.Groups["name"].Value] = match.Groups["value"].Value;
                }
            }
            this.packetBuffer = null;
        }

        private Int32 getIndexOf(Byte[] array, IList value)
        {
            return getIndexOf(array, value, 0);
        }

        private Int32 getIndexOf(Byte[] array, IList value, Int32 startIndex)
        {
            Int32 index = 0;
            Int32 startMatch = Array.IndexOf(array, value[0], startIndex);
            if (startMatch == -1)
                return -1;

            while ((startMatch + index) < array.Length)
            {
                if (array[startMatch + index] == (Byte)value[index])
                {
                    index++;
                    if (index == value.Count)
                        return startMatch;
                }
                else
                {
                    startMatch = Array.IndexOf(array, value[0], startMatch + index);
                    if (startMatch != -1)
                        index = 0;
                    else
                        return -1;
                }
            }
            return -1;
        }

        /// <summary>
        ///  设置文件名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="processType"></param>
        /// <returns></returns>
        private String setFileName(String fileName, ProcessType processType)
        {
            fileName = Path.GetFileName(fileName);
            uploadInfo.FileName = fileName;
            String result = String.Empty;
            if (processType == ProcessType.Save)
            {
                result = Path.Combine(this.filePath, fileName);
            }
            else if (processType == ProcessType.SaveAs)
            {
                String name = Path.GetFileNameWithoutExtension(this.filePath);
                String path = Path.GetDirectoryName(this.filePath);
                String ext = Path.GetExtension(fileName);
                result = Path.Combine(path, String.Concat(name, ext));
            }
            String dirPath = Path.GetDirectoryName(result);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return result;
        }

        private void triggerUpdateEvent()
        {
            if (OnUpdate != null)
            {
                updateEventArgs.Progress = (this.received / this.totalLength).ToString("p");
                OnUpdate(updateEventArgs);
            }
        }
    }
}
