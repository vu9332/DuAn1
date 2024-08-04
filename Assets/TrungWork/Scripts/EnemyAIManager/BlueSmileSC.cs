using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlueSmile", menuName = "ScriptableObjects/EnemyAI/BlueSmile")]
public class BlueSmileSC : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float health;
    public int coins;
    public float experience;
}
