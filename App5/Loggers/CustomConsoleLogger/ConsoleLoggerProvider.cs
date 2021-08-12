using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App5.Loggers.CustomConsoleLogger
{
    [ProviderAlias("ConsoleLogger")]
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        private Dictionary<LogLevel, ConsoleColor> _colors;
        public ConsoleLoggerProvider(IConfiguration configuration) 
        {
            _colors = new Dictionary<LogLevel, ConsoleColor>();
            var cfg = configuration.GetSection("Logging:ConsoleLogger:Colors");
            bool available = false;
            foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
            {
                var val = cfg.GetValue<ConsoleColor?>(level.ToString());
                if (val == null)
                    continue;
                _colors[level] = val ?? ConsoleColor.White;
                available = true;
            }
            if (!available)
                _colors = null;
        }
        public ILogger CreateLogger(string categoryName)
        {
            if (_colors == null)
                return new ConsoleLogger();
            else
                return new ConsoleLogger(_colors);
        }

        public void Dispose()
        {
            
        }
    }
}
