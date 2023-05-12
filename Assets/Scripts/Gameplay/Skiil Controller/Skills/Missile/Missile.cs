using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float destroyAfter = 1;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    public float force = 500;


    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        rigidBody.AddForce(transform.forward * force);
    }

    void OnTriggerEnter(Collider co)
    {
        if (co.CompareTag(GameConst.DamageableObject))
        {
            IDamageable damageableObject;
            if(co.TryGetComponent<IDamageable>(out damageableObject))
            {
                HCDebug.Log("Receive damage");
                damageableObject.ReceiveDamage(10);
                Destroy(gameObject);
            }
        }
    }
}
