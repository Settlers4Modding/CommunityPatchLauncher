using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Commands.DataCommands
{
    class SetYesNoEnumCommand : BaseCommand
    {
        private YesNoEnum decisionEnum;
        private readonly YesNoEnum targetDecision;

        public SetYesNoEnumCommand(YesNoEnum decisionEnum) : this(decisionEnum, YesNoEnum.No)
        {
        }

        public SetYesNoEnumCommand(YesNoEnum decisionEnum, YesNoEnum targetDecision)
        {
            this.decisionEnum = decisionEnum;
            this.targetDecision = targetDecision;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            decisionEnum = targetDecision;
        }
    }
}
