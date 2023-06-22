using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabManager : Singleton<PlayfabManager>
{
    public void SaveUserRemoteData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"", ""}
            }
        };
    }

    public void GetUserRemoteData()
    {

    }
}
