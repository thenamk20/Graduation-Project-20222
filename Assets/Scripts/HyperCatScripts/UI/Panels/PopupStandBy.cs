using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopupStandBy : UIPanel
{
    public static PopupStandBy Instance { get; private set; }

    [SerializeField]
    private Image fillProgress;

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupStandBy;
    }

    public static void Show()
    {
        var newInstance = (PopupStandBy) GUIManager.Instance.NewPanel(UiPanelType.PopupStandBy);
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
        fillProgress.fillAmount = 0;
        DOTween.To(() => fillProgress.fillAmount, x => fillProgress.fillAmount = x, 1, 2f);
        StartCoroutine(DelayHide());
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

    IEnumerator DelayHide()
    {
        yield return new WaitForSecondsRealtime(2f);
        Evm.OnStartBattle.Dispatch();
        yield return new WaitForSeconds(0.5f);
        AudioAssistant.Instance.PlayMusic("Battle");
        Close();
    }
}