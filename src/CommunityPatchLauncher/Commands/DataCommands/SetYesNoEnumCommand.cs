using CommunityPatchLauncher.Enums;
using System;

namespace CommunityPatchLauncher.Commands.DataCommands
{
    class SetYesNoEnumCommand : BaseDataCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

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
