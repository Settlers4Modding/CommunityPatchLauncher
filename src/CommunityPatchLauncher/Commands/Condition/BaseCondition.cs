namespace CommunityPatchLauncher.Commands.Condition
{
    /// <summary>
    /// Abstract class for simpler usage
    /// </summary>
    abstract internal class BaseCondition : ICondition
    {
        /// <inheritdoc/>
        public string ErrorMessage => errorMessage;

        /// <summary>
        /// The error message to return
        /// </summary>
        protected string errorMessage;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public BaseCondition()
        {
            errorMessage = string.Empty;
        }

        /// <inheritdoc/>
        public virtual bool ConditionFailed(object parameter)
        {
            return !ConditionFullfilled(parameter);
        }

        /// <inheritdoc/>
        public abstract bool ConditionFullfilled(object parameter);
    }
}
