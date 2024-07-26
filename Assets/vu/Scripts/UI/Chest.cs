using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   BoxCollider2D box;
    Animator animator;
  // [SerializeField] private bool isOpen;
   

    private void Start()
    {
        animator = GetComponent<Animator>();
     
        // box = GameObject.Fi
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            animator.Play("Chest");
           // isOpen = true;
            EventManager.OpenSkillMenu();
            //EventManager.IsUpdateCard();
       

        }
       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
           
            //isOpen = false;
            animator.Play("Chest Close");
            EventManager.CloseSkillMenu();


        }
       

    }
}
