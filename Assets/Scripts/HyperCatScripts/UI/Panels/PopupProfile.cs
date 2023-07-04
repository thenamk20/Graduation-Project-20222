using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupProfile : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI userNameText;

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
}