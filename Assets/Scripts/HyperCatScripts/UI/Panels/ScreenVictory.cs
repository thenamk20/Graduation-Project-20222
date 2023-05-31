using Photon.Pun;
using UnityEngine.SceneManagement;

public class ScreenVictory : UIPanel
{
    public static ScreenVictory Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.ScreenVictory;
    }

    public static void Show()
    {
        var newInstance = (ScreenVictory) GUIManager.Instance.NewPanel(UiPanelType.ScreenVictory);
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

    public void GoBack()
    {
        //NetworkManager.Instance.LeaveRoom();
        PhotonNetwork.LoadLevel((int)SceneIndex.Hall);
        //SceneManager.LoadSceneAsync((int)SceneIndex.Hall);
    }
}