using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats Config", menuName = "Configs/StatCharacter")]
public class CharacterStatConfig : ScriptableObject
{
    public int health;
    public int chakra;
    public float moveSpeed;
    public float restoreChakraSpeed;
    public int charkaSlots;
}
