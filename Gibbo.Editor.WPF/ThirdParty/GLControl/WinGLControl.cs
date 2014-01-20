#region Copyrights
/*
Gibbo2D License - Version 1.0
Copyright (c) 2013 - Gibbo2D Team
Founders Joao Alves <joao.cpp.sca@gmail.com> & Luis Fernandes <luisapidcloud@hotmail.com>

Permission is granted to use this software and associated documentation files (the "Software") free of charge, 
to any person or company. The code can be used, modified and merged without restrictions, but you cannot sell 
the software itself and parts where this license applies. Still, permission is granted for anyone to sell 
applications made using this software (for example, a game). This software cannot be claimed as your own, 
except for copyright holders. This license notes should also be available on any of the changed or added files.

The software is provided "as is", without warranty of any kind, express or implied, including but not limited to 
the warranties of merchantability, fitness for a particular purpose and non-infrigement. In no event shall the 
authors or copyright holders be liable for any claim, damages or other liability.

The license applies to all versions of the software, both newer and older than the one listed, unless a newer copy 
of the license is available, in which case the most recent copy of the license supercedes all others.

*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using OpenTK.Graphics;
using OpenTK.Platform;

namespace OpenTK
{
    class WinGLControl : IGLControl
    {
        #region P/Invoke declarations

        #region Message

        struct MSG
        {
            public IntPtr HWnd;
            public uint Message;
            public IntPtr WParam;
            public IntPtr LParam;
            public uint Time;
            public POINT Point;
            //internal object RefObject;

            public override string ToString()
            {
                return String.Format("msg=0x{0:x} ({1}) hwnd=0x{2:x} wparam=0x{3:x} lparam=0x{4:x} pt=0x{5:x}", (int)Message, Message.ToString(), HWnd.ToInt32(), WParam.ToInt32(), LParam.ToInt32(), Point);
            }
        }

        #endregion

        #region Point

        struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public System.Drawing.Point ToPoint()
            {
                return new System.Drawing.Point(X, Y);
            }

            public override string ToString()
            {
                return "Point {" + X.ToString() + ", " + Y.ToString() + ")";
            }
        }

        #endregion

        #region PeekMessage

        [System.Security.SuppressUnmanagedCodeSecurity]
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        static extern bool PeekMessage(ref MSG msg, IntPtr hWnd, int messageFilterMin, int messageFilterMax, int flags);

        #endregion

        #region

        #endregion

        #endregion

        #region Fields

        MSG msg = new MSG();
        IWindowInfo window_info;
        GraphicsMode mode;

        #endregion

        #region Constructors

        public WinGLControl(GraphicsMode mode, Control control)
        {
            this.mode = mode;

            window_info = Utilities.CreateWindowsWindowInfo(control.Handle);
        }

        #endregion

        #region IGLControl Members

        public IGraphicsContext CreateContext(int major, int minor, GraphicsContextFlags flags)
        {
            return new GraphicsContext(mode, window_info, major, minor, flags);
        }

        public bool IsIdle
        {
            get { return !PeekMessage(ref msg, IntPtr.Zero, 0, 0, 0); }
        }

        public IWindowInfo WindowInfo
        {
            get
            {
                // This method forces the creation of the control. Beware of this side-effect!
                return window_info;
            }
        }

        #endregion
    }
}
