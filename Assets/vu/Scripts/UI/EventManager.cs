using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {  get; set; }

    public delegate void SkillMenuDeleagte();
    public static event SkillMenuDeleagte OnOpenSkillMenu;
    public static event SkillMenuDeleagte OnCloseSkillMenu;
    //public static event SkillMenuDeleagte OnChooseCard1;
    //public static event SkillMenuDeleagte OnChooseCard2;
    //public static event SkillMenuDeleagte OnChooseCard3;
 
  //  public static event SkillMenuDeleagte OnUpDateSkillMenu;

   // public SkillManager skillManager;
     public  GameObject cardBoard;
    //Transform[] card;
    //[SerializeField]   List< bool> boolObj =  new List<bool>();
   // CardSelectionHandler[] chillObj;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
      
    }
    public static void OpenSkillMenu()
    {
        OnOpenSkillMenu?.Invoke();
    }

    public static void CloseSkillMenu()
    {
        OnCloseSkillMenu?.Invoke();
    }
  
  
    private void OnEnable()
    {
        OnOpenSkillMenu += ShowSkillMenu;
        OnCloseSkillMenu += HideSkillMenu;
        //ChooseSomeThings += CardSelectionManager.instance.Chosse;
        
    }

    private void OnDisable()
    {
       OnOpenSkillMenu -= ShowSkillMenu;
       OnCloseSkillMenu -= HideSkillMenu;
        //ChooseSomeThings -= CardSelectionManager.instance.Chosse;

    }
    void ShowSkillMenu()
    {
        StatusManagement.PressHideUI();
        foreach (Transform item in cardBoard.transform)
        { 
            if (!item.gameObject.GetComponent<CardSelectionHandler>().isSelected)
                    item.gameObject.SetActive(true);
        }

    }
    void HideSkillMenu()
    {
        StatusManagement.PressShowUI();
        foreach (Transform item in cardBoard.transform)
        {
           
                 item.gameObject.SetActive(false);
        }

    }


}
