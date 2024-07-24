using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTwo : MonoBehaviour,ISkill
{
    


    
    PlayerHealth playerHealth;
    PlayerCombat playerCombat;
    Animator myAnimator;

   void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();      
        playerCombat = GetComponent<PlayerCombat>();
        myAnimator = GetComponent<Animator>();
    }
    public void ExecuteSkill(InputAction.CallbackContext context)
    {
       
    }

    #region Skill 2   
    
    #endregion
   
}
