using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using Microsoft.Extensions.Options;

using TradingEngine.Logging.Configuration;

namespace TradingEngine.Logging
{
    public class TextLogger : AbstractLogger, ITextLogger
	{

        private readonly LoggingConfiguration _loggingConfiguration;

        public TextLogger(IOptions<LoggingConfiguration> loggingConfiguration) : base()
        {
            _loggingConfiguration = loggingConfiguration.Value ?? throw new ArgumentNullException(nameof(TradingEngine));
            var now = DateTime.Now;
            string dir = Path.Combine(_loggingConfiguration.TextLoggerConfiguration.Directory, $"{now:yyyy-MM-dd}");
            string name = $"{_loggingConfiguration.TextLoggerConfiguration.Filename}-{now:HH_mm_ss}";
            string nameExtension = Path.ChangeExtension(name, _loggingConfiguration.TextLoggerConfiguration.FileExtension);
            string path = Path.Combine(dir, name);
            Directory.CreateDirectory(dir);
            _ = Task.Run(() => LogAsync(path, _logBuffer, _tokenSource.Token));
		}

        ~TextLogger()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            // Protect from synchronization issues
            lock (_lock)
            {
                if (_disposed)
                {
                    return;
                }
                _disposed = true;
            }
            
            if (disposing)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }
        }

        private static async void LogAsync(string path, BufferBlock<LogInfo> logBuffer, CancellationToken token)
        {
            using var fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            using var streamWriter = new StreamWriter(fileStream) { AutoFlush = true, };
            try
            {
                var item = await logBuffer.ReceiveAsync(token).ConfigureAwait(false);
                string message = FormatLogItem(item);
                await streamWriter.WriteAsync(message).ConfigureAwait(false);
            }
            catch (OperationCanceledException) { }
        }

        protected static string FormatLogItem(LogInfo item)
        {
            return $"[{item.now:yyyy-MM-dd HH-mm-ss.fffffff}] [{item.threadName, -15}:{item.threadID:000}] " +
                $"[{item.level}] {item.message}";
        }

        protected override void Log(LogLevel log, string module, string message)
        {
            _logBuffer.Post(new LogInfo(log, module, message, DateTime.Now, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name));
        }

        private readonly BufferBlock<LogInfo> _logBuffer = new BufferBlock<LogInfo>();
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly object _lock = new object();
        private bool _disposed = false;
    }
}

 