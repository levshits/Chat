using Microsoft.Practices.Prism.Logging;
using NLog;

namespace Chat.Services
{
    public class NLogILoggerFacade:ILoggerFacade
    {
        private static readonly Logger Logger = LogManager.GetLogger("RootLogger");
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                {
                    Logger.Debug(message);
                }
                    break;
                case Category.Info:
                {
                    Logger.Info(message);
                }
                    break;
                case Category.Warn:
                {
                    Logger.Warn(message);
                }
                    break;
                case Category.Exception:
                {
                    Logger.Error(message);
                }
                    break;
            }
        }
    }
}