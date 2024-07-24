using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillThree : MonoBehaviour,ISkill
{

    //public static SkillThree Instance { get; set; } 
    // Start is called before the first frame update
    


    Rigidbody2D rb;
    PlayerHealth playerHealth;
    Animator myAnimator;
    PlayerCombat playerCombat;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();    
        playerHealth = GetComponent<PlayerHealth>();     
        myAnimator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExecuteSkill(InputAction.CallbackContext context)
    {
        
    }
    #region Skill 3
  
    
    
    #endregion
}
