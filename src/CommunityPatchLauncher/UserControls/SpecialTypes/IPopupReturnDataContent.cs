namespace CommunityPatchLauncher.UserControls.SpecialTypes
{
    interface IPopupReturnDataContent : IPopupContent
    {
        T getReturnData<T>();
        object getReturnData();
    }
}
