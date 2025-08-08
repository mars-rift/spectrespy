using System;
using System.Collections.Generic;
using System.IO;

namespace spectrespy.Logging
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }

    public class Logger
    {
        private static Logger? _instance;
        private static readonly object _lock = new object();
        private readonly string _logFilePath;
        private readonly List<LogEntry> _recentLogs = new List<LogEntry>();
        private const int MaxRecentLogs = 100;

        private Logger()
        {
            var logDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SpectreSpy",
                "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            _logFilePath = Path.Combine(logDirectory, $"spectrespy_{DateTime.Now:yyyyMMdd}.log");
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new Logger();
                    }
                }
                return _instance;
            }
        }

        public void Log(LogLevel level, string message, Exception? exception = null)
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = level,
                Message = message,
                Exception = exception?.ToString()
            };

            // Add to recent logs
            lock (_recentLogs)
            {
                _recentLogs.Add(logEntry);
                if (_recentLogs.Count > MaxRecentLogs)
                {
                    _recentLogs.RemoveAt(0);
                }
            }

            // Write to file
            try
            {
                var logLine = $"[{logEntry.Timestamp:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                if (exception != null)
                {
                    logLine += $"\nException: {exception}";
                }

                File.AppendAllText(_logFilePath, logLine + Environment.NewLine);
            }
            catch
            {
                // Ignore file write errors to prevent infinite loops
            }
        }

        public void Debug(string message) => Log(LogLevel.Debug, message);
        public void Info(string message) => Log(LogLevel.Info, message);
        public void Warning(string message) => Log(LogLevel.Warning, message);
        public void Error(string message, Exception? exception = null) => Log(LogLevel.Error, message, exception);

        public List<LogEntry> GetRecentLogs()
        {
            lock (_recentLogs)
            {
                return new List<LogEntry>(_recentLogs);
            }
        }
    }

    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Exception { get; set; }
    }
}
