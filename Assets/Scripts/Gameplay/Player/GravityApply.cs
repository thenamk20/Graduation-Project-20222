using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityApply : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;
    public float gravity = 9.8f;

    private void Update()
    {
        if (!characterController.isGrounded)
        {
            // Apply gravity to the character controller
            Vector3 gravityVector = Vector3.down * gravity * Time.deltaTime;
            characterController.Move(gravityVector);
        }
    }
}
