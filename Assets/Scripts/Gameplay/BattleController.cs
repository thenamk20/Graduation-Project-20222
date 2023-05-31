using Cinemachine;
using Photon.Pun;
using Sigtrap.Relays;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviourPunCallbacks
{
    public CinemachineVirtualCamera followVcam;

    private CinemachineFramingTransposer followVcamFt;

    public static BattleController Instance;

    public List<PlayerManager> Players;

    public Relay OnEndBattle = new Relay();

    private void Start()
    {
        Instance = this;
        followVcamFt = followVcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        Players = new List<PlayerManager>();
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
    }

    public void RemovePlayer(PlayerManager player)
    {
        if (Players.Contains(player))
        {
            Players.Remove(player);
            CheckEndBattle();
        }
    }

    public void CheckEndBattle()
    {
        int countPlayers = 0;
        foreach(var player in Players)
        {
            if (player != null && player.isAlive) countPlayers++;
        }

        HCDebug.Log("Check battle end: " + countPlayers);

        if(countPlayers <= 1)
        {
            OnEndBattle.Dispatch();
        }
    }

    public void ChangeMasterClient()
    {
        
    }
}
