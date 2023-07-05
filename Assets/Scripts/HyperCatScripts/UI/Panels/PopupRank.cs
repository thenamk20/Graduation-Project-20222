using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class PopupRank : UIPanel
{
    public static PopupRank Instance { get; private set; }

    [SerializeField] private LeaderboardItem itemPrefab;

    [SerializeField] private Transform container;

    [SerializeField] private GameObject loading;

    [SerializeField] private GameObject errorNotif;

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupRank;
    }

    public static void Show()
    {
        var newInstance = (PopupRank) GUIManager.Instance.NewPanel(UiPanelType.PopupRank);
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
        loading.gameObject.SetActive(true);
        container.gameObject.SetActive(false);
        errorNotif.gameObject.SetActive(false);

        GetLeaderboard();
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

    public override void Close()
    {
        base.Close();
        foreach(Transform item in container)
        {
            Destroy(item.gameObject);
        }
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "StarPoint",
            StartPosition = 0,
            MaxResultsCount = 20
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderBoardSucceed, OnGetLeaderboardFailed);
    }

    void OnGetLeaderBoardSucceed(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            LeaderboardItem lbItem = Instantiate(itemPrefab, container);
            lbItem.Init((item.Position + 1).ToString(), item.DisplayName, item.StatValue.ToString());
            //Debug.Log(item.DisplayName + "_" + item.StatValue);
        }

        loading.SetActive(false);
        container.gameObject.SetActive(true);
    }

    void OnGetLeaderboardFailed(PlayFabError error)
    {
        HCDebug.Log("Get leader board failed!", HcColor.Red);
        loading.SetActive(false);
        errorNotif.SetActive(true);
    }
}