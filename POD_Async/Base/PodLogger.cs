using NLog;
using NLog.Config;
using System.IO;

namespace POD_Async.Base
{
    public static class PodLogger
    {
        public static Logger Logger { get; private set; }
        private static LoggingConfiguration nLogConfig = new LoggingConfiguration();
        public static string LogPath { get; }

        static PodLogger()
        {
            Logger = LogManager.GetCurrentClassLogger();
            LogPath = Directory.GetCurrentDirectory()+ @"\logs";
            GlobalDiagnosticsContext.Set("logDirectory", $"{LogPath}");
        }

        public static void AddRule(LogLevel logLevel, bool onTxtFile, bool onConsole)
        {
            if (onTxtFile)
            {              
                var logfile = new NLog.Targets.FileTarget()
                {
                    Name = $"{logLevel.Name}-LogFile",
                    FileName = "${gdc:logDirectory}/${level}.txt",
                    Layout = GetLayout()
                };

                nLogConfig.AddRuleForOneLevel(logLevel, logfile);            
            }
           
            if (onConsole)
            {
                var logconsole = new NLog.Targets.ConsoleTarget()
                {
                    Name = $"{ logLevel.Name }-LogConsole",
                    Layout = GetLayout()
                };

                nLogConfig.AddRuleForOneLevel(logLevel, logconsole);
            }

            LogManager.Configuration = nLogConfig;
        }

        private static string GetLayout()
        {
            var layout = "\r\n-----------------------------------------------------------------------------\r\n${longdate} | ${callsite} :\r\n${message}\r\n-----------------------------------------------------------------------------";
            return layout;
        }
    }
}
