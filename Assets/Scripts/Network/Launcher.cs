using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.Runtime.CompilerServices;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        //PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVerion;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToPhoton()
    {
        string nickName = GameManager.Instance.data.user.name;
        PhotonNetwork.GameVersion = "0.0.0";
        ConnectToPhoton(nickName);
    }

    public override void OnConnectedToMaster()
    {
        HCDebug.Log("Connect to server", HcColor.Green);
        HCDebug.Log($"NickName: {PhotonNetwork.LocalPlayer.NickName}", HcColor.Green);
        HCDebug.Log($"Game Version: {PhotonNetwork.GameVersion}", HcColor.Green);

        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void ConnectToPhoton(string nickName)
    {
        Debug.Log($"Connect to Photon as {nickName}");
        PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        HCDebug.Log("Join lobby", HcColor.Aqua);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        HCDebug.Log("Disconnect to server: " + cause.ToString() , HcColor.Red);
    }

    #region PUN callbacks
    public override void OnCreateRoomFailed(short returnCode, string message)
    {

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            RoomInfo currentRoom = NetworkManager.Instance.AllRoomList.Find(x => x.Name == room.Name);
            if (room.RemovedFromList)
            {
                if (currentRoom != null)
                    NetworkManager.Instance.AllRoomList.Remove(room);
            }
            else
            {
                if(currentRoom == null)
                    NetworkManager.Instance.AllRoomList.Add(room);
            }
        }
    }

    #endregion
}
