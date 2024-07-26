using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newData", menuName = "Player/playerData")]

public class PlayerData : ScriptableObject
{
    public float playerHealth;
    public float playerStamina;
    public float playerCoin;
    public float playerExp;

    public bool _isMoving = false;
    public bool _isJumping = false;
    public bool _isRunning = false;
    public bool CanDash = true;
    public float dashCD = 5f;
    public bool _isRolling = false;
    public bool _isWallSliding = false;
} 
