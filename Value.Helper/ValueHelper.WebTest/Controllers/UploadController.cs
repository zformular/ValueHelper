using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper.ValueUpload;
using WebHelper.ValueUpload.Infrastructure;
using WebHelper.ValueUpload.UploadEvents;

namespace ValueHelper.WebTest.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            var path = Server.MapPath("~/Uploads");
            var context = ControllerContext.HttpContext;
            ValueUpload upload = new ValueUpload(context, context.Request.ContentEncoding);
            upload.OnUploading += new Uploading(upload_OnUploading);
            UploadInfo info = upload.Save("uploadfile", path);
            if (info.Success)
            {
                return Content("Success");
            }
            else
            {
                return Content(info.Exception.Message);
            }
        }

        private void upload_OnUploading(object sender, UploadingEventArgs e)
        {
            String progress = e.Progress;
        }

    }
}
