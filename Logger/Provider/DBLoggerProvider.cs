using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Provider
{
    public abstract class DBLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;

        public DBLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return CreateLogger(categoryName, _filter);
        }

        /// <summary>
        /// Logger를 생성합니다.
        /// </summary>
        /// <param name="categoryName">Logger 카테고리 이름</param>
        /// <param name="filter">Filter 함수</param>
        /// <returns>Logger 구현체</returns>
        protected abstract ILogger CreateLogger(string categoryName, Func<string, LogLevel, bool> filter);

        public void Dispose()
        {

        }
    }
}
