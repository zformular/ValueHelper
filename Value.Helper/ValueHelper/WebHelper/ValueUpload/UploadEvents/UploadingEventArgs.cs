using System;

namespace WebHelper.ValueUpload.UploadEvents
{
    public class UploadingEventArgs : EventArgs
    {
        private String progress;
        public String Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
            }
        }
        public UploadingEventArgs()
        {
            this.progress = "0";
        }

        public UploadingEventArgs(String progress)
        {
            this.progress = progress;
        }
    }
}
