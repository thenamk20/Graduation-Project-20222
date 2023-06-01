using Photon.Realtime;

public class PopupQuickMatch : UIPanel
{
    public static PopupQuickMatch Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupQuickMatch;
    }

    public static void Show()
    {
        var newInstance = (PopupQuickMatch) GUIManager.Instance.NewPanel(UiPanelType.PopupQuickMatch);
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