using System.Collections.Generic;
using UnityEngine;

public class PopupAvatars : UIPanel
{
    public static PopupAvatars Instance { get; private set; }

    [SerializeField]
    private AvatarItem avatarItemPrefab;

    [SerializeField]
    private Transform container;

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupAvatars;
    }

    public static void Show()
    {
        var newInstance = (PopupAvatars) GUIManager.Instance.NewPanel(UiPanelType.PopupAvatars);
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
        foreach(var config in Cfg.gameCfg.avatars)
        {
            AvatarItem avatar = Instantiate(avatarItemPrefab, container);
            avatar.Init(config);
        }
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
        isInited = true;
    }
}