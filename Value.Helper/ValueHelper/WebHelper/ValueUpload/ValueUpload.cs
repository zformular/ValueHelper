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
using System.IO;
using System.Web;
using System.Text;
using WebHelper.ValueUpload.UploadEvents;
using WebHelper.ValueUpload.Infrastructure;

namespace WebHelper.ValueUpload
{
    /// <summary>
    ///  实现ASP.NET MVC大数据文件上传
    ///  注意事项:
    ///     调用方法的Action不得显式传参
    ///     但是可在表单内用hidden来传递参数
    ///     参数的读取方式ValueUpload.Form["paramName"];
    /// </summary>
    public sealed class ValueUpload : UploadBase
    {
        public event Uploading OnUploading;
        private UploadingEventArgs uploadingEventArgs = new UploadingEventArgs();

        public ValueUpload(HttpContextBase httpContextBase)
        {
            this.Initialize(httpContextBase, httpContextBase.Request.ContentEncoding);
        }

        public ValueUpload(HttpContextBase httpContextBase, Encoding encoding)
        {
            this.Initialize(httpContextBase, encoding);
        }

        public new UploadInfo Save(String inputName, String filePath)
        {
            return base.Save(inputName, filePath);
        }

        public new UploadInfo SaveAs(String inputName, String fileName)
        {
            return base.SaveAs(inputName, fileName);
        }

        private HttpWorkerRequest getHttpWorkerRequest(HttpContextBase httpContextBase)
        {
            IServiceProvider provider = (IServiceProvider)httpContextBase;
            HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)(provider.GetService(typeof(HttpWorkerRequest)));
            return httpWorkerRequest;
        }

        private void Initialize(HttpContextBase httpContextBase, Encoding encoding)
        {
            base.encoding = encoding;
            base.httpContextBase = httpContextBase;
            base.httpWorkerRequest = getHttpWorkerRequest(httpContextBase);
            base.OnUpdate += new UpdateDelegate(ValueUpload_OnUpdate);
        }

        private void ValueUpload_OnUpdate(UpdateEventArgs e)
        {
            if (OnUploading != null)
            {
                uploadingEventArgs.Progress = e.Progress;
                OnUploading(this, uploadingEventArgs);
            }
        }

    }
}
