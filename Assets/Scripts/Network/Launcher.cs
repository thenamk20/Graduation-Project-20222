using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVerion;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        HCDebug.Log("Connect to server", HcColor.Green);
        HCDebug.Log($"NickName: {PhotonNetwork.LocalPlayer.NickName}", HcColor.Green);
        HCDebug.Log($"Game Version: {PhotonNetwork.GameVersion}", HcColor.Green);

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        HCDebug.Log("Join lobby", HcColor.Aqua);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        HCDebug.Log("Disconnect to server: " + cause.ToString() , HcColor.Red);
    }
}
