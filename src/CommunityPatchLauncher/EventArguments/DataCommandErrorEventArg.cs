using CommunityPatchLauncher.Enums;
using System;

namespace CommunityPatchLauncher.EventArguments
{
    /// <summary>
    /// Data command error arguments
    /// </summary>
    public class DataCommandErrorEventArg : EventArgs
    {
        /// <summary>
        /// The error severity
        /// </summary>
        private readonly ErrorSeverityEnum severity;

        /// <summary>
        /// The error severity
        /// </summary>
        public ErrorSeverityEnum Severity => severity;

        /// <summary>
        /// The error message
        /// </summary>
        private readonly string message;

        /// <summary>
        /// The error message
        /// </summary>
        public string Message => message;

        /// <summary>
        /// The error exception
        /// </summary>
        private readonly Exception exception;

        /// <summary>
        /// The exception throwen
        /// </summary>
        public Exception ThrowenException;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="severity">The error severity</param>
        /// <param name="message">The error message</param>
        public DataCommandErrorEventArg(ErrorSeverityEnum severity, string message) : this(severity, message, null)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="severity">The error severity</param>
        /// <param name="message">The error message</param>
        /// <param name="exception">The throwen exception</param>
        public DataCommandErrorEventArg(ErrorSeverityEnum severity, string message, Exception exception)
        {
            this.severity = severity;
            this.message = message;
            this.exception = exception;
        }
    }
}
