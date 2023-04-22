public class PopupTest : UIPanel
{
    public static PopupTest Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupTest;
    }

    public static void Show()
    {
        var newInstance = (PopupTest) GUIManager.Instance.NewPanel(UiPanelType.PopupTest);
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