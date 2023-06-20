using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using TMPro.Examples;
using UnityEngine;

public abstract class BuffItem : MonoBehaviour
{
    [SerializeField] protected AnimationCurve movingCurve;
    [SerializeField] private PhotonView buffPV;

    private Vector3 offset = new Vector3(0, 1, 0);

    private PlayerController target;

    private bool inited;

    private float time;

    private bool isReached;

    private bool foundTarget;

    public abstract void ClaimBuff(PlayerController player);


    public void Init()
    {
        inited = false;
        time = 0;
        isReached = false;
        transform.DOJump(transform.position + new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, 1f)), 2, 1, 0.7f).OnComplete(() =>
        {
            inited = true;
        });
    }

    private void Update()
    {
        if (!inited) return;

        if(!foundTarget)
        {
            Collider[] _cols = Physics.OverlapSphere(transform.position, 2f);
            foreach(var col in _cols)
            {
                if(col.gameObject.TryGetComponent(out PlayerController _target)){
                    target = _target;

                    if(buffPV.IsMine)
                        buffPV.RPC(nameof(UpdateState), RpcTarget.All);
                }
            }
        }
        else
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, movingCurve.Evaluate(time));

            if (!isReached && Vector3.Distance(gameObject.transform.position, target.transform.position + offset) < 0.3f)
            {
                isReached = true;
                ClaimBuff(target);
                if (buffPV.IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }

    [PunRPC]
    public void UpdateState()
    {
        foundTarget = true;
    }
}
