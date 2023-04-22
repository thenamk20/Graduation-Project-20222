#region

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif
using System;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
    }

    private void Start()
    {
        EventGlobalManager.Instance.OnUpdateSetting.Dispatch();

        LoadingManager.Instance.LoadScene(SceneIndex.Hall, MainScreen.Show);
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

#if PROTOTYPE
            Data.User.PurchasedNoAds = true;
#endif
            Database.SaveData();
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