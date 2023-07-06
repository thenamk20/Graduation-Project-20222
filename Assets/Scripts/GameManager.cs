#region

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using PlayFab.ClientModels;
using PlayFab;

#endregion

public class GameManager : Singleton<GameManager>
{
#if UNITY_EDITOR
    private bool IsPlaying => EditorApplication.isPlaying;
    [ShowIf(nameof(IsPlaying))]
#endif
    public GameData data;
    public bool gameInited;
    //public HCGameSetting gameSetting;

    private int _secondToRemindComeback;

    [HideInInspector] public float lastClaimOnlineGiftTime;

    public static bool EnableAds
    {
        get
        {
            return false;
        }
    }

    public static bool NetworkAvailable => Application.internetReachability != NetworkReachability.NotReachable;

    //#if UNITY_EDITOR
    //    [Button]
    //    private void GetGameSetting()
    //    {
    //        gameSetting = HCTools.GetGameSetting();
    //    }
    //#endif

    protected override void Awake()
    {
        base.Awake();

        LoadGameData();

        SetupPushNotification();

        RequestTrackingForiOs();

        GUIManager.Instance.Init();

        Instance.gameInited = true;

        EventGlobalManager.Instance.OnGameInited.Dispatch();

        LoadRemoteData();
    }

    private void Start()
    {
        EventGlobalManager.Instance.OnUpdateSetting.Dispatch();

        FetchPlayFabData();
    }

    public void FetchPlayFabData()
    {
        var request = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoFailed);

        void OnGetAccountInfoSuccess(GetAccountInfoResult result)
        {
            if (result.AccountInfo != null && result.AccountInfo.TitleInfo != null)
            {
                PlayFabManager.Instance.displayName = result.AccountInfo.TitleInfo.DisplayName;
                if (!string.IsNullOrEmpty(result.AccountInfo.TitleInfo.DisplayName))
                {
                    data.user.name = result.AccountInfo.TitleInfo.DisplayName;
                }

                LoadingManager.Instance.LoadScene(SceneIndex.Hall, MainScreen.Show);

                NetworkManager.Instance.Launcher.ConnectToPhoton();
            }
        }

        void OnGetAccountInfoFailed(PlayFabError error)
        {
            HCDebug.Log("Get account info failed", HcColor.Red);
            LoadingManager.Instance.LoadScene(SceneIndex.Hall, MainScreen.Show);
        }

        PlayFabManager.Instance.GetUserRemoteData();
    }

    public void AddMoney(int value)
    {
        data.user.money += value;
        EventGlobalManager.Instance.OnMoneyChange.Dispatch(true);
    }

    public bool SpendMoney(int value)
    {
        if (data.user.money >= value)
        {
            data.user.money -= value;
            EventGlobalManager.Instance.OnMoneyChange.Dispatch(true);
            return true;
        }

        EventGlobalManager.Instance.OnMoneyChange.Dispatch(false);
        return false;
    }

    private void LoadGameData()
    {
        data = Database.LoadData();
        if (data == null)
        {
            data = new GameData();
        }

        data.user.isRememberMe = PreloadData.Instance.gameData.user.isRememberMe;
        data.user.cachedEmail = PreloadData.Instance.gameData.user.cachedEmail;
        data.user.cachedPassword = PreloadData.Instance.gameData.user.cachedPassword;

        Database.SaveData();
    }

    private void LoadRemoteData()
    {
        string displayName = PlayFabManager.Instance.displayName;
        if (!string.IsNullOrEmpty(displayName))
        {
            data.user.name = displayName;
        }
    }

    private void SetupPushNotification()
    {

    }

    private void RequestTrackingForiOs()
    {
#if UNITY_IOS
        if (!Data.Setting.iOSTrackingRequested)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            Data.Setting.iOSTrackingRequested = true;
            Database.SaveData();
        }
#endif
    }

    private void UpdateGraphicSetting()
    {
        if (data.setting.highPerformance == 1)
        {
            Application.targetFrameRate = 60;
            Screen.SetResolution(Screen.width, Screen.height, true);
        }
        else
        {
            Application.targetFrameRate = 30;
            Screen.SetResolution(Screen.width / 2, Screen.height / 2, true);
        }
    }

    public override void OnApplicationQuit()
    {
        Logout();
        base.OnApplicationQuit();
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            Logout();
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            Logout();
    }

    private void Logout()
    {
        data.user.lastTimeLogOut = DateTime.Now;
        Database.SaveData();
    }

    private void OnEnable()
    {
        EventGlobalManager.Instance.OnUpdateSetting.AddListener(UpdateGraphicSetting);
    }

    private void OnDestroy()
    {
        if (!EventGlobalManager.Instance)
            return;

        EventGlobalManager.Instance.OnUpdateSetting.RemoveListener(UpdateGraphicSetting);
    }

    public void SetupRemindOfflinePushNotification()
    {


    }
}