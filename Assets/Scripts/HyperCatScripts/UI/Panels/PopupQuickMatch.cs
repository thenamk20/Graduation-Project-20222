using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopupQuickMatch : UIPanel, IMatchmakingCallbacks
{
    [SerializeField] private byte maxPlayers = 2;

    public static PopupQuickMatch Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupQuickMatch;
    }

    public static void Show()
    {
        var newInstance = (PopupQuickMatch) GUIManager.Instance.NewPanel(UiPanelType.PopupQuickMatch);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();

        Init();
    }

    private void Init()
    {
        QuickMatch();
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public override void Close()
    {
        base.Close();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            HCDebug.Log("Num count:" + PhotonNetwork.CurrentRoom.PlayerCount, HcColor.Green);
            HCDebug.Log("Is in room:" + PhotonNetwork.InRoom, HcColor.Green);

            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                Close();
                NetworkManager.Instance.StartGame();
            }
        }
    }

    private void CreateRoom()
    {
        string roomName = "QuickMatch - #" + Random.Range(10000, 99999);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions
        {
            MaxPlayers = 6
        });
    }

    private void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void IMatchmakingCallbacks.OnJoinRandomFailed(short returnCode, string message)
    {
        HCDebug.Log("join room failed, create room instead", HcColor.Red);
        CreateRoom();
    }

    void IMatchmakingCallbacks.OnJoinedRoom()
    {
        // joined a room successfully
    }
}