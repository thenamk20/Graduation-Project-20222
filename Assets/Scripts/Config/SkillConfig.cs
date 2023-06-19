using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Config", menuName = "Configs/Skill")]
public class SkillConfig : ScriptableObject
{
    public int skillIndex;
    public float executeTime;
    public float cooldownTime;

    public string title;

    public string description;
}
