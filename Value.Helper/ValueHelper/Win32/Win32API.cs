using System;
using System.Runtime.InteropServices;

namespace ValueHelper.Win32
{
    public static class Win32API
    {
        /// <summary>
        ///  将指定的消息发送到一个或多个窗体(同步方法, PostMessage是异步方法)
        /// </summary>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern Int32 SendMessage(Int32 hwnd, Int32 wMsg, Int32 wParam, [MarshalAs(UnmanagedType.AsAny)]Object lParam);

        /// <summary>
        ///  为窗体指定一个新的位置
        /// </summary>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "SetWindowPos")]
        public static extern Int32 SetWindowPos(Int32 hwnd, Int32 hWndInsertAfter, Int32 x, Int32 y, Int32 cx, Int32 cy, Int32 wFlags);

        /// <summary>
        ///  破坏销毁指定窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern Boolean DestroyWindow(Int32 hwnd);

        /// <summary>
        ///  打开摄像头窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("avicap32.dll")]
        public static extern Int32 capCreateCaptureWindowA(String lpszWindowName, Int32 dwStyle, Int32 x, Int32 y, Int32 nWidth, Int16 nHeight, Int32 hWndParent, Int32 nID);

        /// <summary>
        ///  销毁摄像头窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("avicap32.dll")]
        public static extern Boolean capGetDriverDescriptionA(Int16 wDriver, String lpszName, Int32 cbName, String lpszVer, Int32 cbVer);
    }
}
