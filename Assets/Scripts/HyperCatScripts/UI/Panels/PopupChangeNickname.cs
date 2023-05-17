using Photon.Pun;
using UnityEngine;

public class PopupChangeNickname : UIPanel
{
    private string newNickName;

    public static PopupChangeNickname Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupChangeNickname;
    }

    public static void Show()
    {
        var newInstance = (PopupChangeNickname) GUIManager.Instance.NewPanel(UiPanelType.PopupChangeNickname);
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

    public void ChangeNickName(string _name)
    {
        newNickName = _name;
    }

    public void ConfirmChangeNickName()
    {
        if (string.IsNullOrEmpty(newNickName)) return;

        Gm.data.user.name = newNickName;
        PhotonNetwork.NickName = newNickName;
        EventGlobalManager.Instance.OnChangeName.Dispatch();
        Close();
    }
}