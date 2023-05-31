using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public static MyPlayer Instance;

    void Awake()
    {
        Instance = this;           
    }

    public PlayerManager Manager;

    public PlayerController Controller;

    public void InitMyPlayer(PlayerManager _manager, PlayerController _controller)
    {
        Manager = _manager;
        Controller = _controller;
    }
}
