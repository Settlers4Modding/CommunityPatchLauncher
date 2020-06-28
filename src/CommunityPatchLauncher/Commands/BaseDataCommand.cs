﻿using CommunityPatchLauncher.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands
{
    internal abstract class BaseDataCommand : IDataCommand
    {
        /// <summary>
        /// The data of the command
        /// </summary>
        protected object data;

        /// <inheritdoc/>
        public event EventHandler<DataCommandEventArg> Executed;

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc/>
        public abstract void Execute(object parameter);

        protected virtual void ExecutionDone()
        {
            EventHandler<DataCommandEventArg> handler = Executed;
            DataCommandEventArg args = new DataCommandEventArg(data);
            handler?.Invoke(this, args);
        }
    }
}
