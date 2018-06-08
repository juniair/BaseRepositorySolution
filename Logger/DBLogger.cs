using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public abstract class DBLogger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private bool _selfException = false;
        protected DbContext _context;

        protected DBLogger(string categoryName, Func<string, LogLevel, bool> filter, DbContext context)
        {
            _categoryName = categoryName;
            _filter = filter;
            _context = context;
        }    
        

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (_selfException)
            {
                _selfException = false;
                return;
            }
            _selfException = true;
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);

            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }
            try
            {
                WriteLog(message);
                _selfException = false;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Log를 작성합니다.
        /// </summary>
        /// <param name="message">작성할 message</param>
        protected abstract void WriteLog(string message);


        /// <summary>
        /// 메세지의 최대 길이를 구합니다.
        /// </summary>
        /// <returns></returns>
        protected abstract int? GetMaxMessageLength();
    }
}
