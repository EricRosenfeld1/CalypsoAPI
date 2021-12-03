using CalypsoAPI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CalypsoAPI.Test
{
    [TestClass]
    public class CalypsoStateTest
    {
        private const string ZEISS_MDE_MSG_CNC_START = "ZEISS_MDE_MSG_CNC_START";
        private const string ZEISS_MDE_MSG_CNC_END = "ZEISS_MDE_MSG_CNC_END";
        private const string ZEISS_MDE_MSG_CNC_STOP = "ZEISS_MDE_MSG_CNC_STOP";

        private int WM_ZEISS_MDE_MSG_CNC_START;
        private int WM_ZEISS_MDE_MSG_CNC_END;
        private int WM_ZEISS_MDE_MSG_CNC_STOP;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        private static extern int RegisterWindowMessage(string lpString);

        private Calypso calypso;
        private IntPtr messageWindow;

        [TestInitialize]
        public async Task Initialize()
        {
            WM_ZEISS_MDE_MSG_CNC_START = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_START));
            WM_ZEISS_MDE_MSG_CNC_END = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_END));
            WM_ZEISS_MDE_MSG_CNC_STOP = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_STOP));


            calypso = new Calypso()          
            .Configure(configuration =>
            {
                configuration.CMMObserverFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "files");
            });

            await calypso.Run();

            var windows = OpenWindowGetter.GetOpenWindows();
            foreach (var item in windows)
                if (item.Value == "MessageForm")
                    messageWindow = item.Key;

            Assert.AreNotEqual(IntPtr.Zero, messageWindow);
        }

        [TestMethod]
        public void WindowMessages_Test()
        {
            SendMessage(messageWindow, (uint)WM_ZEISS_MDE_MSG_CNC_START, IntPtr.Zero, IntPtr.Zero);
            Assert.AreEqual(CalypsoAPI.Core.Status.Running, calypso.State.Status);

            SendMessage(messageWindow, (uint)WM_ZEISS_MDE_MSG_CNC_STOP, IntPtr.Zero, IntPtr.Zero);
            Assert.AreEqual(CalypsoAPI.Core.Status.Paused, calypso.State.Status);

            SendMessage(messageWindow, (uint)WM_ZEISS_MDE_MSG_CNC_END, IntPtr.Zero, IntPtr.Zero);
            Assert.AreEqual(CalypsoAPI.Core.Status.Finished, calypso.State.Status);
        }

    }
}
