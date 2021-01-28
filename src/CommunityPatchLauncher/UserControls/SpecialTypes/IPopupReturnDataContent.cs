namespace CommunityPatchLauncher.UserControls.SpecialTypes
{
    interface IPopupReturnDataContent : IPopupContent
    {
        /// <summary>
        /// Get the data returned by the popup
        /// </summary>
        /// <typeparam name="T">The type to cast the data to</typeparam>
        /// <returns>The type casted data</returns>
        T getReturnData<T>();

        /// <summary>
        /// Get the data returned by the popup
        /// </summary>
        /// <returns>Returns the stored data object</returns>
        object getReturnData();
    }
}
