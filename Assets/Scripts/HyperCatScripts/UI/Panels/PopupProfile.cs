using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupProfile : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI userNameText;

    [SerializeField]
    private TextMeshProUGUI matchCountText;

    [SerializeField]
    private TextMeshProUGUI winCountText;

    [SerializeField]
    private TextMeshProUGUI startPointText;

    [SerializeField]
    private Image avatarIcon;

    public static PopupProfile Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupProfile;
    }

    public static void Show()
    {
        var newInstance = (PopupProfile) GUIManager.Instance.NewPanel(UiPanelType.PopupProfile);
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
        userNameText.text = Gm.data.user.name;
        UpdateAvatar(Gm.data.user.userRemoteData.avatarID);

        matchCountText.text = Gm.data.user.userRemoteData.matchCount.ToFormatString();
        winCountText.text = Gm.data.user.userRemoteData.winCount.ToFormatString();
        startPointText.text = Gm.data.user.userRemoteData.rewardPoint.ToFormatString();
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        Evm.OnChangeName.AddListener(UpdateNickName);
        Evm.OnUpdateAvatar.AddListener(UpdateAvatar);
      
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
        Evm.OnChangeName.RemoveListener(UpdateNickName);
        Evm.OnUpdateAvatar.RemoveListener(UpdateAvatar);
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void ChangeName()
    {
        AudioAssistant.Shot(TypeSound.OpenPopup);
        PopupChangeNickname.Show();
    }

    void UpdateNickName()
    {
        userNameText.text = Gm.data.user.name;
    }

    void UpdateAvatar(int avatarId)
    {
        AvatarConfig config = Cfg.gameCfg.avatars.Find(x => x.avatarIndex == avatarId);
        avatarIcon.sprite = config.icon;
    }

    public void OpenPopupAvatar()
    {
        PopupAvatars.Show();
    }

    public void Logout()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadSceneAsync((int)SceneIndex.Login);
        Close();
        GUIManager.Instance.ClearGui();
    }
}