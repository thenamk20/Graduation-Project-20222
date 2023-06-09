using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static GameData;

public class PlayFabManager : Singleton<PlayFabManager>
{
    public string displayName = "";

    private void Start()
    {
        CheckInitRemoteData();
    }

    public void GetPlayFabDisplayName()
    {
        var request = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoFailed);

        void OnGetAccountInfoSuccess(GetAccountInfoResult result){
            if(result.AccountInfo != null && result.AccountInfo.TitleInfo != null)
            {
                displayName = result.AccountInfo.TitleInfo.DisplayName;
            }
        }

        void OnGetAccountInfoFailed(PlayFabError error)
        {
            HCDebug.Log("Get account info failed", HcColor.Red);
        }
    }

    public void GetUserRemoteData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetDataSucceed, OnGetDataFailed);
    }

    void OnGetDataSucceed(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("RemoteData"))
        {
            UserRemoteData userRemoteData = JsonConvert.DeserializeObject<UserRemoteData>(result.Data["RemoteData"].Value);
            GameManager.Instance.data.user.userRemoteData = userRemoteData;
        }
    }

    void OnGetDataFailed(PlayFabError error)
    {

    }

    public void SaveUserRemoteData(UserRemoteData remoteData)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"RemoteData", JsonConvert.SerializeObject(remoteData)}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnUpdateDataSuccess, OnUpdateDataFailed);
    }

    void OnUpdateDataSuccess(UpdateUserDataResult result)
    {
        HCDebug.Log("update remote data successfully", HcColor.Green);
    }

    void OnUpdateDataFailed(PlayFabError error)
    {
        HCDebug.Log("update remote data failed", HcColor.Red);
    }

    public void InitUserRemoteData()
    {
        UserRemoteData newData = new UserRemoteData();
        SaveUserRemoteData(newData);
    }

    public void UpdateUserRemoteData(Action<UserRemoteData, int> updateAction = null, int point = 0)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnUserDataReceived, OnFailedGetData);

        void OnUserDataReceived(GetUserDataResult result)
        {
            if(result.Data != null && result.Data.ContainsKey("RemoteData"))
            {
                UserRemoteData userRemoteData = JsonConvert.DeserializeObject<UserRemoteData>(result.Data["RemoteData"].Value);
                updateAction?.Invoke(userRemoteData, point);
            }
        }

        void OnFailedGetData(PlayFabError error)
        {
            HCDebug.Log("Get Remote Data Failed", HcColor.Red);
        }
    }

    public void UpdateRewardPoints(int gainPoint)
    {
        UpdateUserRemoteData(CallbackUpdateRewardPoint, gainPoint);
    }

    void CallbackUpdateRewardPoint(UserRemoteData remoteData, int rewardPointGain)
    {
        UserRemoteData newData = new UserRemoteData
        {
            avatarID = remoteData.avatarID,
            winCount = remoteData.winCount,
            matchCount = remoteData.matchCount,
            rewardPoint = remoteData.rewardPoint + rewardPointGain,
        };

        SendLeaderBoard(newData.rewardPoint);

        GameManager.Instance.data.user.userRemoteData = newData;
        

        SaveUserRemoteData(newData);
    }

    public void UpdateMatchCount()
    {
        UpdateUserRemoteData(CallbackUpdateMatchCount, 1);
    }

    void CallbackUpdateMatchCount(UserRemoteData remoteData, int matchIncrease = 1)
    {
        UserRemoteData newData = new UserRemoteData
        {
            avatarID = remoteData.avatarID,
            winCount = remoteData.winCount,
            matchCount = remoteData.matchCount + matchIncrease,
            rewardPoint = remoteData.rewardPoint,
        };

        GameManager.Instance.data.user.userRemoteData = newData;

        SaveUserRemoteData(newData);
    }

    public void UpdateWinCount()
    {
        UpdateUserRemoteData(CallbackUpdateWinCount, 1);
    }

    void CallbackUpdateWinCount(UserRemoteData remoteData, int increase = 1)
    {
        UserRemoteData newData = new UserRemoteData
        {
            avatarID = remoteData.avatarID,
            winCount = remoteData.winCount + increase,
            matchCount = remoteData.matchCount,
            rewardPoint = remoteData.rewardPoint,
        };

        GameManager.Instance.data.user.userRemoteData = newData;

        SaveUserRemoteData(newData);
    }

    public void UpdateAvatar(int avatarId)
    {
        UpdateUserRemoteData(CallbackUpdateAvatar, avatarId);
    }

    void CallbackUpdateAvatar(UserRemoteData remoteData, int newAvatar)
    {
        UserRemoteData newData = new UserRemoteData
        {
            avatarID = newAvatar,
            winCount = remoteData.winCount,
            matchCount = remoteData.matchCount,
            rewardPoint = remoteData.rewardPoint,
        };

        GameManager.Instance.data.user.userRemoteData = newData;

        SaveUserRemoteData(newData);
    }

    public void CheckInitRemoteData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnUserDataReceived, OnFailedGetData);

        void OnUserDataReceived(GetUserDataResult result)
        {
            if (result.Data != null && !result.Data.ContainsKey("RemoteData"))
            {
                InitUserRemoteData();
            }
        }

        void OnFailedGetData(PlayFabError error)
        {
            HCDebug.Log("Get Remote Data Failed On Init", HcColor.Red);
        }
    }

    #region Leaderboard
    public void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "StarPoint",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdated, OnLeaderboardUpdateFailed);
    }

    void OnLeaderboardUpdated(UpdatePlayerStatisticsResult result)
    {
        HCDebug.Log("Update leader board succeed!", HcColor.Green);
    }

    void OnLeaderboardUpdateFailed(PlayFabError error)
    {
        HCDebug.Log("Update leader board failed!", HcColor.Red);
    }

    [Button]
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
        foreach(var item in result.Leaderboard)
        {
            Debug.Log(item.DisplayName + "_" + item.StatValue);
        }
    }

    void OnGetLeaderboardFailed(PlayFabError error)
    {
        HCDebug.Log("Get leader board failed!", HcColor.Red);
    }

    #endregion
}
