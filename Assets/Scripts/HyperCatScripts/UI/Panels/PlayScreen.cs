using System.Collections.Generic;
using UnityEngine;

public class PlayScreen : UIPanel
{
    [SerializeField]
    private List<SkillItemUI> skillItemUIList;

    public static PlayScreen Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PlayScreen;
    }

    public static void Show()
    {
        var newInstance = (PlayScreen) GUIManager.Instance.NewPanel(UiPanelType.PlayScreen);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        base.OnAppear();
        Init();
    }

    private void Init()
    {
        foreach(var item in skillItemUIList)
        {
            item.Init();
        }
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void ForceCheckEndBattle()
    {
        BattleController.Instance.RemovePlayer(MyPlayer.Instance.Manager);
        BattleController.Instance.CheckEndBattle();
    }
    
}