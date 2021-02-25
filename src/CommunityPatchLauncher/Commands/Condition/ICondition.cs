namespace CommunityPatchLauncher.Commands.Condition
{
    /// <summary>
    /// Condition instance to check if some conditions are fullfilled
    /// </summary>
    internal interface ICondition
    {
        /// <summary>
        /// The error message for this condition
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// True if the condition is fullfilled
        /// </summary>
        /// <param name="parameter">The parameter to check</param>
        /// <returns>True if fullfilled</returns>
        bool ConditionFullfilled(object parameter);

        /// <summary>
        /// True if the condition failed
        /// </summary>
        /// <param name="parameter">The parameter to check</param>
        /// <returns>True if failing</returns>
        bool ConditionFailed(object parameter);
    }
}
