using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Eagle", menuName = "ScriptableObjects/EnemyAI/Eagle")]
public class EagleSC : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float health;
    public float experience;
}
