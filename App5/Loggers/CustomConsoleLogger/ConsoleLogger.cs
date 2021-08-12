using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App5.Loggers.CustomConsoleLogger
{
    public class ConsoleLogger : ILogger
    {
        private Dictionary<LogLevel, ConsoleColor> _logLevels = new Dictionary<LogLevel, ConsoleColor>
        {
            { LogLevel.Trace, ConsoleColor.Gray },
            { LogLevel.Debug,ConsoleColor.Gray },
            { LogLevel.Information, ConsoleColor.White },
            { LogLevel.Warning, ConsoleColor.Yellow },
            { LogLevel.Error, ConsoleColor.DarkRed },
            { LogLevel.Critical, ConsoleColor.Red }
        };
        public ConsoleLogger() { }
        public ConsoleLogger(Dictionary<LogLevel,ConsoleColor> levels) 
        {
            _logLevels = levels ?? new Dictionary<LogLevel, ConsoleColor>();
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logLevels.ContainsKey(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!_logLevels.ContainsKey(logLevel))
                return;
            Console.ForegroundColor = _logLevels[logLevel];
            Console.Write("{0} [{1}] => ", DateTime.Now.ToString("HH:mm:ss"), logLevel);
            Console.WriteLine(formatter == null ? state.ToString() : formatter(state, exception));
        }
    }
}
