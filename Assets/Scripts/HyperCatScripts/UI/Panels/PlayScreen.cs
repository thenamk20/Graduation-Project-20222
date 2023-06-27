using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayScreen : UIPanel
{
    [SerializeField]
    private Transform skillCtrlContainer;

    [SerializeField]
    private Transform controlPanel;

    [SerializeField]
    private TextMeshProUGUI countPlayerText;

    private GameObject skillsPanel;

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

        if(PopupQuickMatch.Instance != null)
        {
            PopupQuickMatch.Instance.Close();
        }
    }

    private void Init()
    {
        GameObject skillsPanelPrefab = Cfg.characters[Gm.data.user.currentCharacter].skillUIController;
        skillsPanel = Instantiate(skillsPanelPrefab, skillCtrlContainer);

        controlPanel.gameObject.SetActive(true);
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
        if(skillsPanel != null)
        {
            Destroy(skillsPanel);
        }
    }

    public void ForceCheckEndBattle()
    {
        BattleController.Instance.RemovePlayer(MyPlayer.Instance.Manager);
        BattleController.Instance.CheckEndBattle();
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        Evm.OnDoneFighting.AddListener(HideControlSkillPanels);
        Evm.OnStartBattle.AddListener(BindingBattleInfo);
        Evm.OnRemovePlayer.AddListener(BindingBattleInfo);
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
        EventGlobalManager.Instance.OnDoneFighting.RemoveListener(HideControlSkillPanels);
        Evm.OnStartBattle.RemoveListener(BindingBattleInfo);
        Evm.OnRemovePlayer.RemoveListener(BindingBattleInfo);
    }

    void HideControlSkillPanels()
    {
        skillsPanel.gameObject.SetActive(false);
        controlPanel.gameObject.SetActive(false);
    }

    void BindingBattleInfo()
    {
        countPlayerText.text = BattleController.Instance.Players.Count.ToString();
    }
}