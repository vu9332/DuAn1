using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="newSkill",menuName ="Skill/skillData")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public float damage;
    public float coolDownTime;
    public GameObject effectPrefab;
    public List<AudioClip> soundEffect;
}
