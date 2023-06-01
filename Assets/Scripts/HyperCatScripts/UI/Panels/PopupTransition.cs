using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupTransition : UIPanel
{
    [SerializeField]
    private Image loadingFill;

    public static PopupTransition Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupTransition;
    }

    public static void Show()
    {
        var newInstance = (PopupTransition) GUIManager.Instance.NewPanel(UiPanelType.PopupTransition);
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
        loadingFill.fillAmount = 0;
        DOTween.To(() => loadingFill.fillAmount, x => loadingFill.fillAmount = x, 1, 1.25f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            Close();
        });
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