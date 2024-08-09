using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   BoxCollider2D box;
    Animator animator;
    // [SerializeField] private bool isOpen;

    [SerializeField] private GameObject canvas;
   // bool wantOpen=false;
    bool trigger=false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        canvas.SetActive(false);
          
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C)&&trigger)
        {
            canvas.SetActive(false);
            EventManager.OpenSkillMenu();
        }
      
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {


            trigger = true;
            canvas.SetActive(true);
          
            //EventManager.IsUpdateCard();


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            trigger= false;
            canvas.SetActive(false);
            EventManager.CloseSkillMenu();
           // wantOpen = false;


        }
       

    }
}
