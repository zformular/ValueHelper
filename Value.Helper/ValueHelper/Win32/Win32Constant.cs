using System;

namespace ValueHelper.Win32
{
    public static class Win32Constant
    {
        public const Int16 WM_CAP = 0x400;
        public const Int32 WM_CAP_DRIVER_CONNECT = 0x40A;
        public const Int32 WM_CAP_DRIVER_DISCONNECT = 0x40B;
        public const Int32 WM_CAP_EDIT_COPY = 0x41E;
        public const Int32 WM_CAP_SET_PREVIEW = 0x432;
        public const Int32 WM_CAP_SET_PREVIEWRATE = 0x434;
        public const Int32 WM_CAP_SET_SCALE = 0x435;
        public const Int32 WS_CHILD = 0x40000000;
        public const Int32 WS_VISIBLE = 0x10000000;
        public const Int16 SWP_NOMOVE = 0x2;
        public const Int16 SWP_NOSIZE = 0x1;
        public const Int16 SWP_NOZORDER = 0x4;
        public const Int16 HWND_BOTTOM = 0x1;
    }
}
