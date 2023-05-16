using Sirenix.OdinInspector;
using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.Utilities;
using System.Linq;

public class PopupRoom : UIPanel
{
    [SerializeField] private List<GameObject> roomSubPanels; //0: finding, 1: creating, 2: message loading, 3: message error, 4: room inside

    [SerializeField] private TMP_InputField findingRoomNameInput;

    [SerializeField] private TMP_InputField creatingRoomNameInput;

    [SerializeField] private TextMeshProUGUI loadingMessage;

    [SerializeField] private TextMeshProUGUI errorMessage;

    [SerializeField] private TextMeshProUGUI roomNameText;

    [SerializeField] private PlayerRoomListController playerListController;

    [SerializeField] private RoomListController roomListController;

    [SerializeField] private GameObject startGameButton;
    
    public static PopupRoom Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupRoom;
    }

    public static void Show()
    {
        var newInstance = (PopupRoom) GUIManager.Instance.NewPanel(UiPanelType.PopupRoom);
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
        
    }

    public void ShowRoomState(ROOM_PANEL_STATE state)
    {
        for(int i = 0; i < roomSubPanels.Count; i++)
        {
            roomSubPanels[i].SetActive(i == (int)state);
        }

        switch (state)
        {
            case ROOM_PANEL_STATE.INSIDE_ROOM:
               
                break;

            case ROOM_PANEL_STATE.FINDING:
                //roomListController.Init();
                break;
        }
    }

    public void SetLoadingMessage(string message)
    {
        loadingMessage.text = message;
    }

    public void SetErrorMessage(string message)
    {
        errorMessage.text = message;
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

    public void CreateRoom()
    {
        NetworkManager.Instance.CreateRoom(creatingRoomNameInput.text);
        ShowRoomState(ROOM_PANEL_STATE.LOADING);
        SetLoadingMessage("Creating room, please wait...");
    }

    public void LeaveRoom()
    {
        NetworkManager.Instance.LeaveRoom();
    }

    public void FindRoom(string roomName)
    {

    }

    public void StartTheGame()
    {
        NetworkManager.Instance.StartGame();
    }
    

    #region PUn callbacks

    public override void OnJoinedRoom()
    {
        ShowRoomState(ROOM_PANEL_STATE.INSIDE_ROOM);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        EventGlobalManager.Instance.OnEnterARoom.Dispatch();

        Player[] players = PhotonNetwork.PlayerList;

        playerListController.Init(players);

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnLeftRoom()
    {
        Close();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        errorMessage.text = message;
    }

    #endregion
}

public enum ROOM_PANEL_STATE
{
    FINDING = 0,
    CREATING = 1,
    LOADING,
    ERROR,
    INSIDE_ROOM
}