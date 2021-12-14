using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CalypsoAPI.UI
{
    public class UiLogger : ILogger
    {
        private readonly UiLoggerProvider _provider;
        public UiLogger(UiLoggerProvider provider)
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
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if(_provider.Control != null)
            {
                RichTextBox log = _provider.Control as RichTextBox;
                log.Dispatcher.Invoke(new Action(() =>
                {
                    log.AppendText(string.Format("{0} [{1}] {2} {3}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : ""));
                }));
            }   
        }
    }

    public class UiLoggerProvider : ILoggerProvider
    {
        public readonly Control Control;
        public UiLoggerProvider(Control logControl)
        {
            Control = logControl;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new UiLogger(this);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }


    public static class Extensions
    {
        public static ILoggingBuilder AddUiLoggerBuilder(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, UiLoggerProvider>();
            return builder;
        }
    }
}
