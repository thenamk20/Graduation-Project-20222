using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PopupChangeNickname : UIPanel
{
    [SerializeField]
    private GameObject warningText;

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
        warningText.SetActive(false);
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

        if (warningText.activeInHierarchy)
        {
            warningText.SetActive(false);
        }
    }

    public void ConfirmChangeNickName()
    {
        if (string.IsNullOrEmpty(newNickName)) return;
        SubmitNamePlayfab(newNickName);
       
    }

    public void SubmitNamePlayfab(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateSuccess, OnError);
    }

    void OnUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        HCDebug.Log("Update name successed", HcColor.Green);
        Gm.data.user.name = newNickName;
        PhotonNetwork.NickName = newNickName;
        EventGlobalManager.Instance.OnChangeName.Dispatch();
        Close();
    }

    void OnError(PlayFabError error)
    {
        HCDebug.Log("Update name failed", HcColor.Red);
        warningText.SetActive(true);
    }
}