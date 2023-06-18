using TMPro;
using UnityEngine;

public class PopupProfile : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI userNameText;

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
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        Evm.OnChangeName.AddListener(UpdateNickName);
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
        Evm.OnChangeName.RemoveListener(UpdateNickName);
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
}