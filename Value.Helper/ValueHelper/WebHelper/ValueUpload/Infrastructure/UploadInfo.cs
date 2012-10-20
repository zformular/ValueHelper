using System;
using System.Collections.Generic;

namespace WebHelper.ValueUpload.Infrastructure
{
    public class UploadInfo
    {
        public UploadInfo()
        {
            form = new Dictionary<string, string>();
        }

        public String FileName { get; set; }

        public Double FileLength { get; set; }

        private Dictionary<String, String> form;
        public Dictionary<String, String> Form
        {
            get
            {
                return form;
            }
            set
            {
                form = value;
            }
        }

        public Boolean Success { get; set; }

        public Exception Exception { get; set; }
    }
}
