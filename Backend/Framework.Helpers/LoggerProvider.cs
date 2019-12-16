using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Framework.Helpers
{
    public static class DbContextExtensions
    {

        public static void LogSql(this DbContext context, Action<string> logAction)
        {
            var serviceProvider = context.GetInfrastructure<IServiceProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new LoggerProvider(logAction));
        }
    }

    public class LoggerProvider : ILoggerProvider
    {
        private readonly Action<string> logAction;

        public LoggerProvider(Action<string> logAction)
        {
            if (logAction == null)
                throw new ArgumentNullException("logAction");

            this.logAction = logAction;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(logAction);
        }

        public void Dispose()
        {
        }
    }

    public class Logger : ILogger
    {
        private readonly Action<string> logAction;

        public Logger(Action<string> logAction)
        {
            if (logAction == null)
                throw new ArgumentNullException("logAction");

            this.logAction = logAction;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;

        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            logAction(message);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            logAction(message);
        }
    }
}
