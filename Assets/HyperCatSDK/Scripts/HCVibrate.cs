#region

#endregion

namespace HyperCatSdk
{
    public static class HCVibrate
    {
        public static void Haptic(HcHapticTypes type)
        {
           
        }
    }
}

public enum HcHapticTypes
{
    Selection,
    Success,
    Warning,
    Failure,
    LightImpact,
    MediumImpact,
    HeavyImpact,
    RigidImpact,
    SoftImpact,
    None
}