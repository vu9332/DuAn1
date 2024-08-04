using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrapeProjectitle", menuName = "ScriptableObjects/EnemyAI/GrapeProjectitle")]
public class GrapeProjectitle : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float health;
    public int coins;
    public float experience;
}
