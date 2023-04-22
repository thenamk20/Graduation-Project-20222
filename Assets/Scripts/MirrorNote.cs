using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MirrorNote : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/**
  
    - NetworkIdentity: put NetworkIdentity to all prefab spawned in runtime
    - NetworkAuthority: by default, server own alls object, 3 ways to give client authority
    - NetworkManager Callbacks:
        + Server
            - OnStartSever
            - OnServerSceneChanged
        + Client
 
    - Register all prefabs in NetworkManger at Awake function
    
    - [Command]: call on client, run on server: sent player'object from client to player'object from server

    - [ClientRpc]: call on server, invoke on all clients

    - [TargetRpc]
    
**/