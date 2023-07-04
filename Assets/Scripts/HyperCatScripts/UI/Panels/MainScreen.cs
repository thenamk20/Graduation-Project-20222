#region

using Photon.Pun;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

public class MainScreen : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI currentNickname;

    [SerializeField]
    private Image avatarIcon;

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

        UpdateAvatar(Gm.data.user.userRemoteData.avatarID);
;       CharacterPreview.Instance.ToggleCharacterPreview(true);
    }

    public void ShowSetting()
    {
        PopupSetting.Show();
    }

    public void StartGame()
    {
        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        Evm.OnChangeName.AddListener(UpdateNickNameUI);
        Evm.OnUpdateAvatar.AddListener(UpdateAvatar);
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
        Evm.OnChangeName.RemoveListener(UpdateNickNameUI);
        Evm.OnUpdateAvatar.RemoveListener(UpdateAvatar);
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

    public void QuickMatching()
    {
        PopupQuickMatch.Show();
      
    }

    void UpdateNickNameUI()
    {
        currentNickname.text = PhotonNetwork.NickName;
    }

    public void ShowChooseCharacters()
    {
        PopupCharacters.Show();
    }

    public void OpenLeaderboard()
    {
        PopupRank.Show();
    }

    void UpdateAvatar(int avatarId)
    {
        AvatarConfig config = Cfg.gameCfg.avatars.Find(x => x.avatarIndex == avatarId);
        avatarIcon.sprite = config.icon;
    }
}