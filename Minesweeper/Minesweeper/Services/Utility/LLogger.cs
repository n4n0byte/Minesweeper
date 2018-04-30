using NLog;

namespace Minesweeper.Services.Utility {

    /// <summary>
    /// Logging class
    /// </summary>
    public class LLogger : ILogger {

        /// <summary>
        /// Returns instance of Logger
        /// </summary>
        /// <remarks>Currently uses NLog</remarks>
        /// <returns></returns>
        public static Logger GetLogger() {
            return LogManager.GetLogger("loggerRules");
        }

        /// <summary>
        /// Prints With Level Set to Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public void Debug(string message, string arg = null) {
            // Print Message with or without arg
            if (arg == null) GetLogger().Debug(message);
            else GetLogger().Debug(message, arg);
        }

        /// <summary>
        /// Prints With Level Set to Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public void Info(string message, string arg = null) {
            // Print Message with or without arg
            if (arg == null) GetLogger().Info(message);
            else GetLogger().Info(message, arg);            
        }

        /// <summary>
        /// Prints With Level Set to warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public void Warning(string message, string arg = null) {
            // Print Message with or without arg
            if (arg == null) GetLogger().Warn(message);
            else GetLogger().Warn(message, arg);
        }

        /// <summary>
        /// Prints With Level Set to Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public void Error(string message, string arg = null) {
            // Print Message with or without arg
            if (arg == null) GetLogger().Error(message);
            else GetLogger().Error(message, arg);
        }
    }
}