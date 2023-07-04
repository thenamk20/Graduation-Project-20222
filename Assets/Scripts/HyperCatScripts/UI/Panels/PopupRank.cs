public class PopupRank : UIPanel
{
    public static PopupRank Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupRank;
    }

    public static void Show()
    {
        var newInstance = (PopupRank) GUIManager.Instance.NewPanel(UiPanelType.PopupRank);
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
}