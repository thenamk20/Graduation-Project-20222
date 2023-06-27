using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == (int)SceneIndex.Battle) // We're in the game scene
        {
            PlayScreen.Show();
            PhotonNetwork.Instantiate(Path.Combine(GameConst.PhotonPrefabs, GameConst.PlayerManager), Vector3.zero, Quaternion.identity);
            PopupRoom.Instance?.Close();
            PopupStandBy.Show();
        }

        if(scene.buildIndex == (int)SceneIndex.Hall)
        {
            MainScreen.Show();
        }
    }
}