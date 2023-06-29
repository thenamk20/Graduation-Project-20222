using Cinemachine;
using Photon.Pun;
using Sigtrap.Relays;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviourPunCallbacks
{
    private CinemachineFramingTransposer followVcamFt;

    public CinemachineVirtualCamera followVcam;

    public static BattleController Instance;

    public List<PlayerManager> Players;

    public Relay OnEndBattle = new Relay();

    public bool ended = false;

    public ObjectSpawner Spawner;

    public int playersJoinBattle;

    public int CurrentPlayersCount => Players.Count;

    private void Start()
    {
        Instance = this;
        followVcamFt = followVcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        Players = new List<PlayerManager>();
        playersJoinBattle = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public void SetCamWatchMyPlayer(Transform myPlayer)
    {
        followVcam.Follow = myPlayer;
    }

    private void Update()
    {
        
    }

    public void AddPlayer(PlayerManager player)
    {
        Players.Add(player);
        if(PhotonNetwork.IsMasterClient) {

            Vector3 pos = Spawner.GetAnAvailableSpawnPosition();
            player.GetAnSpawnPoint(pos);
        }
    }

    public void RemovePlayer(PlayerManager player)
    {
        if (Players.Contains(player))
        {
            Players.Remove(player);
            CheckEndBattle();
            EventGlobalManager.Instance.OnRemovePlayer.Dispatch();
        }
    }

    public void CheckEndBattle()
    {
        if (!ended)
        {
            int countPlayers = 0;
            foreach (var player in Players)
            {
                if (player != null && player.isAlive) countPlayers++;
            }

            HCDebug.Log("Check battle end: " + countPlayers, HcColor.Green);

            if (countPlayers <= 1)
            {
                ended = true;
                OnEndBattle.Dispatch();
            }
        }
    }
}
