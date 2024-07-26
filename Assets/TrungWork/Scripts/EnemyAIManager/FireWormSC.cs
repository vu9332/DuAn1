using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireWorm", menuName = "ScriptableObjects/EnemyAI/FireWorm")]
public class FireWormSC : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float health;
}
