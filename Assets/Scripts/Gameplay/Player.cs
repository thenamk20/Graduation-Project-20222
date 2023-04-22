using CnControls;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private CharacterController characterController;

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        if (isLocalPlayer)
        {
            LocalBattleController.Instance.SetCamWatchMyPlayer(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            Vector3 dir = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
            if(dir != Vector3.zero && Vector3.SqrMagnitude(dir) > 0.05f)
                CmdMovePlayer(dir * speed);
        }
    }

    [Command]
    public void CmdMovePlayer(Vector3 dir)
    {
        //TODO: logic check
        MovePlayer(dir);
    }

    [TargetRpc]
    public void MovePlayer(Vector3 dir)
    {
        characterController.SimpleMove(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }
}
