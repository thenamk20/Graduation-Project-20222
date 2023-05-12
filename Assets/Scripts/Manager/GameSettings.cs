using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "0.0.0";

    [SerializeField]
    private string _nickName = "Player";

    public string GameVerion => _gameVersion;

    public string NickName
    {
        get
        {
            return $"{_nickName}{Random.Range(0, 100000)}";
        }
    }
}
