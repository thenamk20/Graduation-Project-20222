﻿public enum SceneIndex
{
    None = -1,
    PreloadData = 0,
    Login ,
    Splash,
    Hall,
    Battle
}

public enum PanelType
{
    None,
    Screen,
    Popup,
    Notification,
    Loading
}

public enum TypeSound
{
    None = -1,
    Button = 0,
    OpenPopup = 1,
    ClosePopup = 2,
    Pop,
    Bump,
    Bell,
    BallTap,
    Bloop,
    ClickError,
    Victory,
    Defeat,
    Claim,
    Toat,
    Tick,
    FiroMissileExplode,
    FiroSkill2,
    FiroUltiExplode,
    HaamuSkill1,
    HammuSkill2,
    HammuSkill3
}

public enum Pool
{
    None
}

public enum RewardType
{
    Coin,
    Gift
}

public enum EasingType
{
    Linear = 1,
    InSine,
    OutSine,
    InOutSine,
    InQuad,
    OutQuad,
    InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    InQuart,
    OutQuart,
    InOutQuart,
    InQuint,
    OutQuint,
    InOutQuint,
    InExpo,
    OutExpo,
    InOutExpo,
    InCirc,
    OutCirc,
    InOutCirc,
    InElastic,
    OutElastic,
    InOutElastic,
    InBack,
    OutBack,
    InOutBack,
    InBounce,
    OutBounce,
    InOutBounce,
    Flash,
    InFlash,
    OutFlash,
    InOutFlash,
    Custom
}