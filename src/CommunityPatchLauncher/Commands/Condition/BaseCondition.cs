
namespace CommunityPatchLauncher.Commands.Condition
{
    /// <summary>
    /// Abstract class for simpler usage
    /// </summary>
    abstract internal class BaseCondition : ICondition
    {
        /// <inheritdoc/>
        public virtual bool ConditionFailed(object parameter)
        {
            return !ConditionFullfilled(parameter);
        }

        /// <inheritdoc/>
        public abstract bool ConditionFullfilled(object parameter);
    }
}
