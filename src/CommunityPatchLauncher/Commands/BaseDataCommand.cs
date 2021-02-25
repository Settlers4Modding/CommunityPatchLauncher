using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.EventArguments;
using System;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This class is a abstract base class which will implement the base functions
    /// </summary>
    public abstract class BaseDataCommand : BaseCommand, IDataCommand
    {
        /// <summary>
        /// The data of the command
        /// </summary>
        protected object data;

        /// <inheritdoc/>
        public event EventHandler<DataCommandEventArg> Executed;

        /// <inheritdoc/>
        public event EventHandler<DataCommandErrorEventArg> Error;

        /// <summary>
        /// Was the execution successful
        /// </summary>
        protected virtual void ExecutionDone()
        {
            EventHandler<DataCommandEventArg> handler = Executed;
            DataCommandEventArg args = new DataCommandEventArg(data);
            handler?.Invoke(this, args);
        }

        /// <summary>
        /// Throw an error
        /// </summary>
        /// <param name="severity">The severity to use</param>
        /// <param name="message">The error message</param>
        protected virtual void ThrowError(ErrorSeverityEnum severity, string message)
        {
            ThrowError(severity, message, null);
        }

        /// <summary>
        /// Throw an error
        /// </summary>
        /// <param name="severity">The severity to use</param>
        /// <param name="message">The error message</param>
        /// <param name="exception">The throwen exception</param>
        protected virtual void ThrowError(ErrorSeverityEnum severity, string message, Exception exception)
        {
            ThrowError(new DataCommandErrorEventArg(severity, message, exception));
        }

        /// <summary>
        /// Throw an error
        /// </summary>
        /// <param name="args">The arguments to use</param>
        protected virtual void ThrowError(DataCommandErrorEventArg args)
        {
            EventHandler<DataCommandErrorEventArg> handler = Error;
            handler?.Invoke(this, args);
        }
    }
}
