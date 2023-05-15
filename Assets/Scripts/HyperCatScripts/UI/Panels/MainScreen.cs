#region

using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class MainScreen : UIPanel
{
    public static MainScreen Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.MainScreen;
    }

    public static void Show()
    {
        var newInstance = (MainScreen) GUIManager.Instance.NewPanel(UiPanelType.MainScreen);
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

    public void ShowSetting()
    {
        AudioAssistant.Shot(TypeSound.Button);
        PopupSetting.Show();
    }

    public void StartGame()
    {
        AudioAssistant.Shot(TypeSound.Button);

        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }
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

    public void OpenFindRoom()
    {
        PopupRoom.Show();
        PopupRoom.Instance.ShowRoomState(ROOM_PANEL_STATE.FINDING);
    }

    public void OpenCreateRoom()
    {
        PopupRoom.Show();
        PopupRoom.Instance.ShowRoomState(ROOM_PANEL_STATE.CREATING);
    }
}