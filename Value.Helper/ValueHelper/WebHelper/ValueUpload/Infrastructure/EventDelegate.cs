using System;
using WebHelper.ValueUpload.UploadEvents;

namespace WebHelper.ValueUpload.Infrastructure
{
    // 基类委托
    /// <summary>
    ///  基类更新状态
    /// </summary>
    /// <param name="e"></param>
    public delegate void UpdateDelegate(UpdateEventArgs e);

    // ValueUpload委托
    /// <summary>
    ///  ValueUpload上传过程更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Uploading(Object sender, UploadingEventArgs e);
}
