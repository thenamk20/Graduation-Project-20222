
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRoomListController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform playerListContainer;

    [SerializeField]
    private GameObject playerItemPrefab;

    public void Init(Player[] players)
    {
        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerItemPrefab, playerListContainer).GetComponent<PlayerRoomItem>().Init(players[i]);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        foreach(Transform player in playerListContainer)
        {
            Destroy(player.gameObject);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerItemPrefab, playerListContainer).GetComponent<PlayerRoomItem>().Init(newPlayer);
    }
}
