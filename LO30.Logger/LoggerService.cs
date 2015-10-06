using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Diagnostics;

namespace LO30.Data.Services
{
    public class LogService
    {
       /* private LogWriter _logger;

        public LogService()
        {
            _logger = CreateLogger();
        }

        public LogWriter CreateLogger()
        {
            // Formatter
            TextFormatter briefFormatter = new TextFormatter("{timestamp}  {message} {title} {dictionary({key} - {value}{newline})}");

            // Trace Listener
            var flatFileTraceListener = new FlatFileTraceListener(
              @"C:\Temp\FlatFile.log",
              "----------------------------------------",
              "----------------------------------------",
              briefFormatter);

            var consoleTraceListener = new ConsoleTraceListener();

            // Build Configuration
            var config = new LoggingConfiguration();

            config.AddLogSource("DiskFiles", SourceLevels.All, true)
              .AddTraceListener(flatFileTraceListener);

            config.AddLogSource("Console", SourceLevels.All, true)
                .AddTraceListener(consoleTraceListener);


            return new LogWriter(config);
        }

        public void Write(object message)
        {
           _logger.Write(message);
        }*/
    }
}
