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

        /// <summary>
        /// Was the execution successful
        /// </summary>
        protected virtual void ExecutionDone()
        {
            EventHandler<DataCommandEventArg> handler = Executed;
            DataCommandEventArg args = new DataCommandEventArg(data);
            handler?.Invoke(this, args);
        }
    }
}
