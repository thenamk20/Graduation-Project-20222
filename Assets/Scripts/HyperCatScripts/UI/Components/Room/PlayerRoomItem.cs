using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRoomItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerName;

    private Player player;

    public void Init(Player _player)
    {
        player = _player;
        playerName.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
            EventGlobalManager.Instance.OnPlayersRoomChange.Dispatch();
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
