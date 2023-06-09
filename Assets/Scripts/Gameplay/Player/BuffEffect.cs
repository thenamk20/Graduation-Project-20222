using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : MonoBehaviour
{
    [SerializeField]
    private ReceiveBuffItem buffItemPrefab;

    [SerializeField] private List<Color> buffColorList;

    [SerializeField] private Transform spawnBuffItemUIPoint;

    [SerializeField] private List<GameObject> buffVFXPrefab;

    private PlayerController _owner;

    // Start is called before the first frame update
    void Start()
    {
        _owner = GetComponentInParent<PlayerController>();
    }

    public void ShowBuffEffectUIItem(ItemBuff buffType, int amount)
    {
        ReceiveBuffItem buffItemUI = Instantiate(buffItemPrefab, spawnBuffItemUIPoint);

        switch(buffType)
        {
            case ItemBuff.Damage:
                buffItemUI.Init($"+ {amount}% damage", buffColorList[0]);
                InitVFX(buffVFXPrefab[0]);
                break;

            case ItemBuff.Health:
                buffItemUI.Init($"+ {amount} max health", buffColorList[1]);
                InitVFX(buffVFXPrefab[1]);
                break;

            case ItemBuff.Restore:
                buffItemUI.Init($"+ {amount} health", buffColorList[2]);
                InitVFX(buffVFXPrefab[2]);
                break;

            case ItemBuff.AttackSpeed:
                buffItemUI.Init($"+ {amount}% mana restore", buffColorList[3]);
                InitVFX(buffVFXPrefab[3]);
                break;

            case ItemBuff.MoveSpeed:
                buffItemUI.Init($"+ {amount}% move speed", buffColorList[4]);
                InitVFX(buffVFXPrefab[4]);
                break;

            case ItemBuff.Upgrade:
                buffItemUI.Init($"+ 1 upgrade point", buffColorList[5]);
                InitVFX(buffVFXPrefab[5]);
                break;
        }
    }

    void InitVFX(GameObject obj)
    {
        BuffItemVFX vfxItem = NetworkManager.Instance.InstantiateObject(obj, transform.position, Quaternion.identity).GetComponent<BuffItemVFX>();
        vfxItem.Init(transform, _owner.PhotonView.ViewID);
    }
}

public enum ItemBuff
{
    Damage = 0,
    Health = 1,
    Restore = 2,
    AttackSpeed,
    MoveSpeed,
    Upgrade
}
