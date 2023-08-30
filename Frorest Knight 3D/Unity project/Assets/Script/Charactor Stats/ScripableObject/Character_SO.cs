using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data" , menuName ="Character Stats/Data")]
public class Character_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int maxHealth;

    public int currentHealth;

    public int baseDefence;

    public int currentDefence;

    public float currentSP;
}
