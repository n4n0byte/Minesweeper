namespace Minesweeper.Services.Utility {

    /// <summary>
    /// interface spec for logging 
    /// info
    /// </summary>
    public interface ILogger {
        /**
         * Should Write Debug Level Log Info 
         * to file
         */
        void Debug(string message, string arg = null);

        /**
         * Should Write Info Level Log Info
         * to file
         */
        void Info(string message, string arg = null);

        /**
         * Should Write Warning Level Log Info
         * to file
         */
        void Warning(string message, string arg = null);

        /**
         * Should Write Debug Level Log Info
         * to file
         */
        void Error(string message, string arg = null);
    }
}
