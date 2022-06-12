using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Khronos;

namespace GLEngine
{
    internal static class Program
    {

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();
            string envDebug = Environment.GetEnvironmentVariable("DEBUG");
            Console.WriteLine(envDebug);
            //if (envDebug == "GL")
            //{
            //    Khronos.KhronosApi.Log += delegate (object sender, Khronos.KhronosLogEventArgs e)
            //    {
            //        Console.WriteLine(e.ToString());
            //    };
            //    Khronos.KhronosApi.LogEnabled = true;
            //}
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            // 释放
            FreeConsole();
        }
    }
}
