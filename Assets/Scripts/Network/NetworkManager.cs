using Photon.Pun;
using Photon.Realtime;
using Sigtrap.Relays;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    [SerializeField]
    private Launcher launcher;

    public Launcher Launcher => launcher;

    public List<RoomInfo> AllRoomList;

    private void Start()
    {
        AllRoomList = new List<RoomInfo>();
        EventGlobalManager.Instance.OnEnterARoom.AddListener(ClearListRoom);
    }

    public void CreateRoom(string roomName)
    {
        if (string.IsNullOrEmpty(roomName)) return;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions
        {
            MaxPlayers = 6
        });

        PopupRoom.Show();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public void QuickMatch()
    {
        PhotonNetwork.JoinOrCreateRoom("QuickMatch", new RoomOptions
        {
            MaxPlayers = 2
        }, TypedLobby.Default);
    }

    public void ClearListRoom()
    {
        AllRoomList.Clear();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel((int)SceneIndex.Battle);
    }

    public GameObject InstantiateObject(GameObject item, Vector3 position, Quaternion rotation)
    {
        return PhotonNetwork.Instantiate(Path.Combine(GameConst.PhotonPrefabs, item.name), position, rotation);
    }

    public GameObject InstantiateRoomObject(GameObject item, Vector3 position, Quaternion rotation)
    {
        return PhotonNetwork.InstantiateRoomObject(Path.Combine(GameConst.PhotonPrefabs, item.name), position, rotation);
    }
}
