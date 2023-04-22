using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.MyGame
{
    public class MyNetworkManager : NetworkManager
    {
        public override void OnClientConnect()
        {
            base.OnClientConnect();
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {

        }
    }
}

