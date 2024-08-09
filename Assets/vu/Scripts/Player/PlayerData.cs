using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newData", menuName = "Player/playerData")]

public class PlayerData : ScriptableObject
{
    [Header("Player Thong tin")]
    [Space(10)]
    [Header("Mau")]
    public float playerHealth;
    public float playerCurrentHealth;
    [Space(10)]
    [Header("Mana")]
    public float playerStamina;
    public float playerCurrentStamina;
    [Space(10)]
    [Header("Coin")]
    public float playerCoin;
    [Space(10)]
    [Header("level")]
    public int playerLevel;
    public int playeMaxLevel;
    public float playerExp;
    [Space(10)]
    [Header("Damage")]
  //  public float playerMaxDamage;
    public float playerDamage;


    [Header("Player Controll")]
    public bool _isMoving = false;
    public bool _isJumping = false;
    public bool _isRunning = false;
    public bool _isDash = false;
    public float dashCD = 5f;
    public bool _isRolling = false;
    public bool _isWallSliding = false;

    [Header("Player Skill Unlock")]
    public bool _isSkillThreeUnlock = false;
    public bool _isSkillTwoUnlock = false;
    public bool _isSkillOneUnlock = false;

    [Header("Count Death")]
    public float CountDeath;
} 
