using NLog;
using System;

namespace Puma.Prey.Common.Logging
{
    public static class Log
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Info(Guid errorCode, Exception ex)
        {
            log.Info(string.Format("{0}: {1}", errorCode, ex));
        }
    }
}
