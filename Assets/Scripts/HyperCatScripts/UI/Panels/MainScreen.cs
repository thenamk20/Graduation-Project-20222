#region

using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class MainScreen : UIPanel
{
    public static MainScreen Instance { get; private set; }

    [SerializeField] private GameObject startHostButton;

    [SerializeField] private GameObject startClientButton;

    [SerializeField] private GameObject startServerButton;

    public override UiPanelType GetId()
    {
        return UiPanelType.MainScreen;
    }

    public static void Show()
    {
        var newInstance = (MainScreen) GUIManager.Instance.NewPanel(UiPanelType.MainScreen);
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
        ToggleButtons(true);
    }

    public void ShowSetting()
    {
        AudioAssistant.Shot(TypeSound.Button);
        PopupSetting.Show();
    }

    public void StartGame()
    {
        AudioAssistant.Shot(TypeSound.Button);

        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }
    }


    public void StartHost()
    {
        NetworkManager.singleton.StartHost();
        HCDebug.Log("Start Host");
        ToggleButtons(false);
        StartGame();
    }

    public void StartClient()
    {
        NetworkManager.singleton.StartClient();
        HCDebug.Log("Start Client");
        ToggleButtons(false);
        StartGame();
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartServer();
        HCDebug.Log("Start Server");
        ToggleButtons(false);
        StartGame();
    }

    void ToggleButtons(bool isEnable)
    {
        startHostButton.SetActive(isEnable);
        startClientButton.SetActive(isEnable);
        startServerButton.SetActive(isEnable);
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