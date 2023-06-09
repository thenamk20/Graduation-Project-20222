﻿#region

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

#endregion

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game")]
public class GameConfig : ScriptableObject
{
    [TabGroup("Extra Feature")]
    public ExtraFeatureConfig extraFeatureConfig;

    [TabGroup("Other")]
    public int maxOfflineRemindMinute = 720;

    [TabGroup("Gameplay")]
    public int numberOfPlayerRequire = 2;

    public List<AvatarConfig> avatars;
}