using CommunityPatchLauncher.EventArguments;
using System;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// A command which will return some data
    /// </summary>
    public interface IDataCommand : ICommand
    {
        /// <summary>
        /// Was the command executed
        /// </summary>
        event EventHandler<DataCommandEventArg> Executed;
    }
}
