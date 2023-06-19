#region

using Photon.Pun;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class MainScreen : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI currentNickname;

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
        if (string.IsNullOrEmpty(Gm.data.user.name))
        {
            PopupChangeNickname.Show();
            currentNickname.text = "";
        }
        else
        {
            currentNickname.text = Gm.data.user.name;
        }


        CharacterPreview.Instance.ToggleCharacterPreview(true);
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
        EventGlobalManager.Instance.OnChangeName.AddListener(UpdateNickNameUI);
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
        EventGlobalManager.Instance.OnChangeName.RemoveListener(UpdateNickNameUI);
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
        CharacterPreview.Instance.ToggleCharacterPreview(false);
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

    void UpdateNickNameUI()
    {
        currentNickname.text = PhotonNetwork.NickName;
    }

    public void ShowChooseCharacters()
    {
        PopupCharacters.Show();
    }
}