using NLog;
using TimeKeeper.API.Services;

namespace TimeKeeper.LOG
{
    public class LoggerService
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
            MailService.Send("mersiha996@gmail.com", "Error", message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
            MailService.Send("mersiha996@gmail.com", "Fatal error", message);
        }
    }
}