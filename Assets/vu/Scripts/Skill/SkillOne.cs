using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillOne : MonoBehaviour, ISkill
{
    //public static SkillOne Instance { get; set; }

  
    [SerializeField] private AudioClip attackSoundClip;





    //TouchingDirection touchingDirection;
    PlayerHealth playerHealth;
    PlayerCombat playerCombat;
    Animator myAnimator;

    private void Start()
    {
        //touchingDirection=GetComponent<TouchingDirection>();
        playerHealth = GetComponent<PlayerHealth>();
        //rb=GetComponent<Rigidbody2D>();
        playerCombat = GetComponent<PlayerCombat>();
        myAnimator = GetComponent<Animator>();


    }


    public void ExecuteSkill(InputAction.CallbackContext context)
    {
       
    }
    #region Skill 1
   
    #endregion

}


