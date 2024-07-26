using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bossDemon", menuName = "ScriptableObjects/bossDemon")]
public class bossDemon : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float damageSkill;
    public float health;
}
