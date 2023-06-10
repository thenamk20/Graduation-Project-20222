using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Config", menuName = "Configs/Character")]
public class CharacterConfig : ScriptableObject
{
    public CHARACTER characterID;

    public GameObject characterPrefab;

    public GameObject skillUIController;
}

public enum CHARACTER
{
    ARCHER,
    ASSASSIN,
    MAGE,
    KNIGHT
}
