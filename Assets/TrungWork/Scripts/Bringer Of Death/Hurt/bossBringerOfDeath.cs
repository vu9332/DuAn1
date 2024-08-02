using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bossBringerOfDeath", menuName = "ScriptableObjects/bossBringerOfDeath")]
public class bossBringerOfDeath : ScriptableObject
{
    public string Name;
    public float damageAttack;
    public float damageSkill;
    public float health;
    public int amountExperiencesReceived;
    public int amountCoinsReveived;
}
