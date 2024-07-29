using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bossFlyingEyes",menuName = "ScriptableObjects/bossFlyingEyes")]
public class bossFlyingEyes : ScriptableObject
{
    public string Name;
    public float damage;
    public float health;
    public float damageBite;
    public int amountExperiencesReceived;
}
