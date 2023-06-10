using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControlPanel : MonoBehaviour
{
    [SerializeField]
    private List<SkillItemUI> skillItemUIList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in skillItemUIList)
        {
            item.Init();
        }
    }
}
