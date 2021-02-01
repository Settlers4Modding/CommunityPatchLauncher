using CommunityPatchLauncher.Enums;
using System;

namespace CommunityPatchLauncher.Commands.DataCommands
{
    /// <summary>
    /// This command will set either a yes or no to a given enum
    /// </summary>
    class SetYesNoEnumCommand : BaseDataCommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            data = null;
            YesNoEnum newData = YesNoEnum.No;
            if (Enum.TryParse(parameter.ToString(), out newData))
            {
                data = newData;
            }
            ExecutionDone();
        }
    }
}
