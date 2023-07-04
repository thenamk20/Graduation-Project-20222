using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarItem : HCMonoBehaviour
{
    [SerializeField] private Image avatarIcon;

    private int avatarId;

    public void Init(AvatarConfig avatarConfig)
    {
        avatarIcon.sprite = avatarConfig.icon;
        avatarId = avatarConfig.avatarIndex;
    }

    public void Choose()
    {
        PlayFabManager.Instance.UpdateAvatar(avatarId);
        Evm.OnUpdateAvatar.Dispatch(avatarId);
        PopupAvatars.Instance.Close();
    }
}
