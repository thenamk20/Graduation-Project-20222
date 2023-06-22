#region

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

#endregion

[Serializable]
public class GameData
{
    public SettingData setting = new SettingData();
    public UserData user = new UserData();

    [Serializable]
    public class UserData
    {
        public DateTime lastTimeLogOut = DateTime.Now;
        
        public int level = 1;

        //Progress Data
        public int money;
        public int score;
        public string name = "";
        public int luckyWheelProgress;
        public bool isConsentUpdated;
        public DateTime lastFreeSpinTime = DateTime.MinValue;
        public int dailyRewardClaimedCount;
        public DateTime lastDailyRewardClaimTime = DateTime.MinValue;


        //Leaderboard
        public int totalScoreOffset;

        //Other Data
        public int sessionPlayed;
        private int _keyCount;

        public int currentCharacter;

        public int KeyCount
        {
            get => _keyCount;
            set => _keyCount = Mathf.Clamp(value, 0, 3);
        }

        public bool isRememberMe = false;

        public string cachedEmail;

        public string cachedPassword;

        public UserRemoteData userRemoteData = new UserRemoteData();
    }

    public class UserRemoteData 
    {
        public int avatarID;
        public int winCount;
        public int matchCount;
    }


    [Serializable]
    public class SettingData
    {
        public bool enablePn;
        public bool requestedPn;

        public bool haptic = true;
        public float soundVolume = 1;
        public float musicVolume = 1;
        
        public int highPerformance = 1;

        public bool iOsTrackingRequested;
    }
}

public static class Database
{
    private static string dataKey = "GameData";

    public static void SaveData()
    {
        var dataString = JsonConvert.SerializeObject(GameManager.Instance.data);
        PlayerPrefs.SetString(dataKey, dataString);
        PlayerPrefs.Save();
    }

    public static GameData LoadData()
    {
        if (!PlayerPrefs.HasKey(dataKey))
            return null;

        return JsonConvert.DeserializeObject<GameData>(PlayerPrefs.GetString(dataKey));
    }
}