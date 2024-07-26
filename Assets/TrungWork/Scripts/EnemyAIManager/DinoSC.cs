using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dino", menuName = "ScriptableObjects/EnemyAI/Dino")]
public class DinoSC : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float health;
}
