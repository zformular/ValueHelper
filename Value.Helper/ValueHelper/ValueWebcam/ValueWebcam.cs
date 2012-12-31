using System;
using System.Windows.Forms;
using ValueHelper.Win32;
using System.Drawing;

namespace ValueHelper.ValueWebcam
{
    public class ValueWebcam
    {
        private Int32 posX, posY, width;
        private Int16 height;

        private PictureBox content;
        public PictureBox Content { get { return content; } }

        private Int32 device = 0;
        private Int32 hHwnd;

        public ValueWebcam(Int32 posX, Int32 posY, Int32 width, Int16 height)
        {
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;

            this.content = new PictureBox();
            this.content.Width = width;
            this.content.Height = height;
        }

        public void OpenWebcam()
        {
            // 在容器中打开预览窗体
            hHwnd = Win32API.capCreateCaptureWindowA(
                device.ToString(),
                (Win32Constant.WS_VISIBLE | Win32Constant.WS_CHILD),
                posX, posY, width, height, content.Handle.ToInt32(), 0);

            // 连接设备
            if (Win32API.SendMessage(hHwnd, Win32Constant.WM_CAP_DRIVER_CONNECT, device, 0) == 1)
            {
                // 设置预览窗口大小
                Win32API.SendMessage(hHwnd, Win32Constant.WM_CAP_SET_SCALE, 1, 0);

                // 设置预览比特率(单位毫秒)
                Win32API.SendMessage(hHwnd, Win32Constant.WM_CAP_SET_PREVIEWRATE, 66, 0);

                // 开始从相机内捕捉预览图片
                Win32API.SendMessage(hHwnd, Win32Constant.WM_CAP_SET_PREVIEW, 1, 0);

                // 重置窗体大小,适应图片框
                Win32API.SetWindowPos(hHwnd, Win32Constant.HWND_BOTTOM, posX, posY, width, height, (Win32Constant.SWP_NOMOVE | Win32Constant.SWP_NOZORDER));
            }
            else
            {
                // 发生错误则关闭窗口
                Win32API.DestroyWindow(hHwnd);
            }
        }

        public void CloseWebcam()
        {
            // 与设备断开连接
            Win32API.SendMessage(hHwnd, Win32Constant.WM_CAP_DRIVER_DISCONNECT, device, 0);

            // 销毁窗口
            Win32API.DestroyWindow(hHwnd);
        }

        public Bitmap PrintScreen()
        {
            Win32API.SendMessage(this.hHwnd, Win32Constant.WM_CAP_EDIT_COPY, 0, 0);
            IDataObject obj = Clipboard.GetDataObject();
            if (obj.GetDataPresent(typeof(Bitmap)))
                return (Bitmap)obj.GetData(typeof(Bitmap));
            else
                return null;
        }
    }
}
