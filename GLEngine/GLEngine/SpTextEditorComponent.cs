using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GLEngine
{
    class SpTextEditorComponent : System.Windows.Forms.UserControl
    {

        #region IME関係

        private const int WM_IME_COMPOSITION = 0x010F;
        private const int GCS_RESULTREADSTR = 0x0200;
        private const int WM_IME_STARTCOMPOSITION = 0x10D; // IME変換開始
        private const int WM_IME_ENDCOMPOSITION = 0x10E;   // IME変換終了
        private const int WM_IME_NOTIFY = 0x0282;
        private const int WM_IME_SETCONTEXT = 0x0281;
        // 追加コード
        const uint CFS_RECT = 0x0001;

        public enum ImmAssociateContextExFlags : uint
        {
            IACE_CHILDREN = 0x0001,
            IACE_DEFAULT = 0x0010,
            IACE_IGNORENOCONTEXT = 0x0020
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct C_RECT
        {
            public int _Left;
            public int _Top;
            public int _Right;
            public int _Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct C_POINT
        {
            public int x;
            public int y;
        }

        const uint CFS_POINT = 0x0002;

        public struct COMPOSITIONFORM
        {
            public uint dwStyle;
            public C_POINT ptCurrentPos;
            public C_RECT rcArea;
        }

        [DllImport("Imm32.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("Imm32.dll")]
        private static extern int ImmGetCompositionString(IntPtr hIMC, int dwIndex, StringBuilder lpBuf, int dwBufLen);
        [DllImport("Imm32.dll")]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
        [DllImport("imm32.dll")]
        private static extern IntPtr ImmCreateContext();
        [DllImport("imm32.dll")]
        private static extern bool ImmAssociateContextEx(IntPtr hWnd, IntPtr hIMC, ImmAssociateContextExFlags dwFlags);
        [DllImport("imm32.dll")]
        public static extern int ImmSetCompositionWindow(IntPtr hIMC, ref COMPOSITIONFORM lpCompositionForm);

        IntPtr himc = IntPtr.Zero;


        private void InitializeComponent()
        {
            this.SuspendLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (himc != IntPtr.Zero)
            {
                ImmReleaseContext(this.Handle, himc);
                himc = IntPtr.Zero;
            }
            base.Dispose(disposing);
        }

        ~SpTextEditorComponent()
        {
            if (himc != IntPtr.Zero)
            {
                ImmReleaseContext(this.Handle, himc);
                himc = IntPtr.Zero;
            }
        }
        

        protected override void WndProc(ref Message m)
        {
            //Console.WriteLine("fzy aaa");
            switch (m.Msg)
            {
                case WM_IME_SETCONTEXT:
                    {
                        //Imeを関連付ける
                        IntPtr himc = ImmCreateContext();
                        ImmAssociateContextEx(this.Handle, himc, ImmAssociateContextExFlags.IACE_DEFAULT);
                        base.WndProc(ref m);
                        break;
                    }
                case WM_IME_STARTCOMPOSITION:
                    {
                        //入力コンテキストにアクセスするためのお約束
                        IntPtr hImc = ImmGetContext(this.Handle);

                        //コンポジションウィンドウの位置を設定
                        COMPOSITIONFORM info = new COMPOSITIONFORM();
                        info.dwStyle = CFS_POINT;
                        info.ptCurrentPos.x = 10;
                        info.ptCurrentPos.y = 10;
                        ImmSetCompositionWindow(hImc, ref info);

                        // 追加コード(IMEウィンドウ領域の設定)
                        info.dwStyle = CFS_RECT;
                        info.rcArea._Left = 10;
                        info.rcArea._Top = 10;
                        info.rcArea._Right = 100;
                        info.rcArea._Bottom = 100;
                        ImmSetCompositionWindow(hImc, ref info);

                        //コンポジションウィンドウのフォントを設定
                        //ImmSetCompositionFont(hImc, m_Focus->GetFont()->GetInfoLog());

                        //入力コンテキストへのアクセスが終了したらロックを解除する
                        ImmReleaseContext(Handle, hImc);

                        base.WndProc(ref m);
                        break;
                    }

                default:
                    //IME以外のメッセージは元のプロシージャで処理
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion


        public SpTextEditorComponent()
        {
            InitializeComponent();
        }
    }
}
