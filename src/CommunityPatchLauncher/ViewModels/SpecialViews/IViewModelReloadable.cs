namespace CommunityPatchLauncher.ViewModels.SpecialViews
{
    /// <summary>
    /// Is a model reloadable
    /// </summary>
    public interface IViewModelReloadable
    {
        /// <summary>
        /// The model was loaded again
        /// </summary>
        void Reload();
    }
}
