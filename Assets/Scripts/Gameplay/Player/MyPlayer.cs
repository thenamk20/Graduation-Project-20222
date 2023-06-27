using Sigtrap.Relays;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public static MyPlayer Instance;

    public Relay OnFinishSetupMyPlayer = new Relay();

    void Awake()
    {
        Instance = this;           
    }

    [HideInInspector]
    public PlayerManager Manager;

    [HideInInspector]
    public PlayerController Controller;

    public void InitMyPlayer(PlayerManager _manager, PlayerController _controller)
    {
        Manager = _manager;
        Controller = _controller;
        OnFinishSetupMyPlayer.Dispatch();
    }
}
