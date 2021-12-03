using CalypsoAPI.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CalypsoAPI
{
    public class MessageForm : Form
    {
        private const string ZEISS_MDE_MSG_CNC_START = "ZEISS_MDE_MSG_CNC_START";
        private const string ZEISS_MDE_MSG_CNC_END = "ZEISS_MDE_MSG_CNC_END";
        private const string ZEISS_MDE_MSG_CNC_ERROR = "ZEISS_MDE_MSG_CNC_ERROR";
        private const string ZEISS_MDE_MSG_CNC_STOP = "ZEISS_MDE_MSG_CNC_STOP";
        private const string ZEISS_MDE_MSG_CNC_CONT = "ZEISS_MDE_MSG_CNC_CONT";
        private const string ZEISS_MDE_MSG_CNC_EXIT = "ZEISS_MDE_MSG_CNC_EXIT";
        private const string ZEISS_MDE_MSG_EXCEPTION = "ZEISS_MDE_MSG_EXCEPTION";
        private const string ZEISS_MDE_MSG_CMM_ERROR = "ZEISS_MDE_MSG_CMM_ERROR";
        private const string ZEISS_MDE_MSG_CMM_CLEAR = "ZEISS_MDE_MSG_CMM_CLEAR";
        private const string ZEISS_MDE_MSG_CALYPSO_READY = "ZEISS_MDE_MSG_CALYPSO_READY";
        private const string ZEISS_MDE_MSG_CALYPSO_EXIT = "ZEISS_MDE_MSG_CALYPSO_EXIT";

        private int WM_ZEISS_MDE_MSG_CNC_START;
        private int WM_ZEISS_MDE_MSG_CNC_END;
        private int WM_ZEISS_MDE_MSG_CNC_ERROR;
        private int WM_ZEISS_MDE_MSG_CNC_STOP;
        private int WM_ZEISS_MDE_MSG_CNC_CONT;
        private int WM_ZEISS_MDE_MSG_CNC_EXIT;
        private int WM_ZEISS_MDE_MSG_EXCEPTION;
        private int WM_ZEISS_MDE_MSG_CMM_ERROR;
        private int WM_ZEISS_MDE_MSG_CMM_CLEAR;
        private int WM_ZEISS_MDE_MSG_CALYPSO_READY;
        private int WM_ZEISS_MDE_MSG_CALYPSO_EXIT;

        private Dictionary<int, string> _messages { get; set; } = new Dictionary<int, string>();

        internal event EventHandler<CmmStateChangedEventArgs> CmmStateChanged;

        internal MessageForm()
        {
            InitializeComponent();

            WM_ZEISS_MDE_MSG_CNC_START = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_START));
            WM_ZEISS_MDE_MSG_CNC_END = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_END));
            WM_ZEISS_MDE_MSG_CNC_ERROR = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_ERROR));
            WM_ZEISS_MDE_MSG_CNC_STOP = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_STOP));
            WM_ZEISS_MDE_MSG_CNC_CONT = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_CONT));
            WM_ZEISS_MDE_MSG_CNC_EXIT = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CNC_EXIT));
            WM_ZEISS_MDE_MSG_EXCEPTION = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_EXCEPTION));
            WM_ZEISS_MDE_MSG_CMM_ERROR = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CMM_ERROR));
            WM_ZEISS_MDE_MSG_CMM_CLEAR = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CMM_CLEAR));
            WM_ZEISS_MDE_MSG_CALYPSO_READY = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CALYPSO_READY));
            WM_ZEISS_MDE_MSG_CALYPSO_EXIT = RegisterWindowMessage(nameof(ZEISS_MDE_MSG_CALYPSO_EXIT));

            _messages.Add(WM_ZEISS_MDE_MSG_CALYPSO_EXIT, ZEISS_MDE_MSG_CALYPSO_EXIT);
            _messages.Add(WM_ZEISS_MDE_MSG_CALYPSO_READY, ZEISS_MDE_MSG_CALYPSO_READY);
            _messages.Add(WM_ZEISS_MDE_MSG_CMM_CLEAR, ZEISS_MDE_MSG_CMM_CLEAR);
            _messages.Add(WM_ZEISS_MDE_MSG_CMM_ERROR, ZEISS_MDE_MSG_CMM_ERROR);
            _messages.Add(WM_ZEISS_MDE_MSG_CNC_CONT, ZEISS_MDE_MSG_CNC_CONT);
            _messages.Add(WM_ZEISS_MDE_MSG_CNC_END, ZEISS_MDE_MSG_CNC_END);
            _messages.Add(WM_ZEISS_MDE_MSG_CNC_START, ZEISS_MDE_MSG_CNC_START);
            _messages.Add(WM_ZEISS_MDE_MSG_CNC_STOP, ZEISS_MDE_MSG_CNC_STOP);
            _messages.Add(WM_ZEISS_MDE_MSG_CNC_ERROR, ZEISS_MDE_MSG_CNC_ERROR);
            _messages.Add(WM_ZEISS_MDE_MSG_CNC_EXIT, ZEISS_MDE_MSG_CNC_EXIT);
            _messages.Add(WM_ZEISS_MDE_MSG_EXCEPTION, ZEISS_MDE_MSG_EXCEPTION);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MessageForm
            // 
            this.ClientSize = new System.Drawing.Size(20, 20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageForm";
            this.Text = "MessageForm";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MessageForm_Load);
            this.ResumeLayout(false);

        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            var value = m.Msg;

            if (_messages.ContainsKey(value))
            {
                var message = _messages[value];
                switch (message)
                {
                    case ZEISS_MDE_MSG_CNC_CONT:
                    case ZEISS_MDE_MSG_CNC_START:
                        OnCmmStateChanged(new CmmStateChangedEventArgs() { Status = Status.Running });
                        break;

                    case ZEISS_MDE_MSG_CNC_STOP:
                        OnCmmStateChanged(new CmmStateChangedEventArgs() { Status = Status.Paused });
                        break;

                    case ZEISS_MDE_MSG_CNC_EXIT:
                        OnCmmStateChanged(new CmmStateChangedEventArgs() { Status = Status.Stopped });
                        break;

                    case ZEISS_MDE_MSG_CNC_END:
                        OnCmmStateChanged(new CmmStateChangedEventArgs() { Status = Status.Finished });
                        break;

                    case ZEISS_MDE_MSG_CMM_ERROR:
                        OnCmmStateChanged(new CmmStateChangedEventArgs() { Status = Status.Exception });
                        break;

                    default:
                        break;
                }
            }

            base.WndProc(ref m);
        }

        protected virtual void OnCmmStateChanged(CmmStateChangedEventArgs e)
        {
            CmmStateChanged?.Invoke(this, e);
        }

        [DllImport("User32.dll")]
        private static extern int RegisterWindowMessage(string lpString);

        private void MessageForm_Load(object sender, EventArgs e)
        {

        }
    }


}
