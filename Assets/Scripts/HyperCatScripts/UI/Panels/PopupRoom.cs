using Sirenix.OdinInspector;
using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.Utilities;
using System.Linq;
using DG.Tweening;

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
        ShowRoomState(ROOM_PANEL_STATE.LOADING);
        loadingMessage.text = message;
    }

    public void SetErrorMessage(string message)
    {
        errorMessage.text = message;
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        Evm.OnPlayersRoomChange.AddListener(HandlePlayersRoomChange);
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
        Evm.OnPlayersRoomChange.RemoveListener(HandlePlayersRoomChange);
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void CreateRoom()
    {
        NetworkManager.Instance.CreateRoom(creatingRoomNameInput.text);
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
        PhotonNetwork.CurrentRoom.IsVisible = false;
        NetworkManager.Instance.StartGame();
    }
    
    void HandlePlayersRoomChange()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(PhotonNetwork.CurrentRoom.PlayerCount >= Cfg.gameCfg.numberOfPlayerRequire);
        }
    }

    #region PUn callbacks

    public override void OnJoinedRoom()
    {
        ShowRoomState(ROOM_PANEL_STATE.INSIDE_ROOM);
        roomNameText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;

        EventGlobalManager.Instance.OnEnterARoom.Dispatch();

        Player[] players = PhotonNetwork.PlayerList;

        playerListController.Init(players);

        HCDebug.Log("players in room: " + PhotonNetwork.PlayerList.Count().ToString(), HcColor.Aqua);
        startGameButton.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= Cfg.gameCfg.numberOfPlayerRequire);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        ShowRoomState(ROOM_PANEL_STATE.ERROR);
        AudioAssistant.Shot(TypeSound.ClickError);

        errorMessage.text = "join room failed";

        DOTween.Kill(this);
        DOVirtual.DelayedCall(2f, () =>
        {
            ShowRoomState(ROOM_PANEL_STATE.FINDING);
        }).SetTarget(this);
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
        ShowRoomState(ROOM_PANEL_STATE.ERROR);
        errorMessage.text = "create room failed";
        AudioAssistant.Shot(TypeSound.ClickError);

        DOTween.Kill(this);
        DOVirtual.DelayedCall(2f, () =>
        {
            ShowRoomState(ROOM_PANEL_STATE.CREATING);
        }).SetTarget(this);
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