public class PopupProfile : UIPanel
{
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

    public void ChangeName()
    {
        PopupChangeNickname.Show();
    }
}