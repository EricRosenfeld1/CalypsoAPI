using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace CalypsoAPI.Core
{
    public class UILogger : ILogger
    {
        private readonly UILoggerProvider _provider;

        public UILogger(UILoggerProvider provider)
        {
            _provider = provider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var logRecord = string.Format("{0} [{1}] {2} {3} \n", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");
            if (_provider.Options.LogControl != null)
            {
                var ctl = _provider.Options.LogControl;
                ctl.Invoke((MethodInvoker)(() =>
                   {
                       ctl.AppendText(logRecord);
                   }));
            }
        }
    }

    public class UILoggerOptions
    {
        public RichTextBox LogControl { get; set; }
    }

    public class UILoggerProvider : ILoggerProvider
    {
        public readonly UILoggerOptions Options;

        public UILoggerProvider(UILoggerOptions options)
        {
            Options = options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new UILogger(this);
        }

        public void Dispose()
        {

        }
    }

}
