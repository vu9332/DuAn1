using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {  get; set; }

    public delegate void SkillMenuDeleagte();
    public static event SkillMenuDeleagte OnOpenSkillMenu;
    public static event SkillMenuDeleagte OnCloseSkillMenu;

    public  GameObject cardBoard;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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
    }

    private void OnDisable()
    {
       OnOpenSkillMenu -= ShowSkillMenu;
       OnCloseSkillMenu -= HideSkillMenu;
    }
    void ShowSkillMenu()
    {
        cardBoard.gameObject.SetActive(true);
      
    }
    void HideSkillMenu()
    {
        cardBoard.gameObject.SetActive(false);
      
    }
}
