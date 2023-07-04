using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvatarConfig", menuName = "Configs/Avatars")]
public class AvatarConfig : ScriptableObject
{
    public Sprite icon;
    public int avatarIndex;
}
