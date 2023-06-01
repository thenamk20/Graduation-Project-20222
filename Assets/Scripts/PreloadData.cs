using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadData : Singleton<PreloadData>
{
    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = Database.LoadData();
        if (gameData == null)
        {
            gameData = new GameData();
        }

        SceneManager.LoadSceneAsync((int)SceneIndex.Login);
    }
}
