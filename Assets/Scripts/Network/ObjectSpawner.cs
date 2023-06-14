using Photon.Pun;
using PlayFab.ExperimentationModels;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private List<Transform> chestInitPoints;

    [SerializeField]
    private List<Transform> characterSpawnPoints;

    [SerializeField]
    private GameObject chestPrefab;

    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private GameObject damageAreaPrefab;

    private List<Vector3> availableOnes;

    public DamageArea DamageArea;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            InitChests();
            InitDamageArea();
        }
    }

    private void Awake()
    {
        availableOnes = new List<Vector3>();

        foreach (var item in characterSpawnPoints)
        {
            availableOnes.Add(item.position);
        }
    }

    void InitChests()
    {
        foreach (var item in chestInitPoints)
        {
            NetworkManager.Instance.InstantiateRoomObject(chestPrefab, item.position, Quaternion.identity);
        }
    }

    public Vector3 GetAnAvailableSpawnPosition()
    {
        HCDebug.Log($"spawn available count: {availableOnes.Count}", HcColor.Green);
        if(availableOnes.Count > 0)
        {
            Vector3 pos = availableOnes.GetRandom();
            availableOnes.Remove(pos);
            return pos;
        }
        return Vector3.zero;
    }

    void InitDamageArea()
    {
        DamageArea = NetworkManager.Instance.InstantiateRoomObject(damageAreaPrefab, new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)), Quaternion.identity)
            .GetComponent<DamageArea>();

        PV.RPC(nameof(ShareDamageAreaReference), RpcTarget.Others, DamageArea.gameObject.GetPhotonView().ViewID);
    }

    [PunRPC]
    void ShareDamageAreaReference(int viewID)
    {
        DamageArea = PhotonView.Find(viewID).gameObject.GetComponent<DamageArea>();
    }
}
