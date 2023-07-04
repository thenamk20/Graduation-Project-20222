using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerRoomItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerName;

    [SerializeField] private TextMeshProUGUI avatarIndexText;

    private Player player;

    public void Init(Player _player)
    {
        player = _player;
        playerName.text = _player.NickName;

        if (_player == PhotonNetwork.LocalPlayer)
        {
            SetAvatar(GameManager.Instance.data.user.userRemoteData.avatarID);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
            EventGlobalManager.Instance.OnPlayersRoomChange.Dispatch();
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    public void SetAvatar(int avatarIndex)
    {
        Hashtable hash = new Hashtable
        {
            { "avatar", avatarIndex }
        };

        HCDebug.Log(hash);

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(player == targetPlayer)
        {
            if (changedProps.ContainsKey("avatar"))
            {
                if (changedProps.TryGetValue("avatar", out object avatar))
                {
                    avatarIndexText.text = ((int)avatar).ToString();
                }
            }
        }
    }
}
