using System;

namespace WebHelper.ValueUpload.UploadEvents
{
    public class UpdateEventArgs : EventArgs
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
        public UpdateEventArgs()
        {
            this.progress = "0";
        }

        public UpdateEventArgs(String progress)
        {
            this.progress = progress;
        }
    }
}
