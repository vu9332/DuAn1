using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlackSmile", menuName = "ScriptableObjects/EnemyAI/BlackSmile")]
public class BlackSmileSC : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float health;
}
