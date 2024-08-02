using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static EventManager;
using static UnityEngine.Rendering.DebugUI;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager instance;
    public GameObject[] card;
   // private bool[] isCardActive;
   // [SerializeField] private PlayerData playerData;
  

    void Start()
    {
    
        if (instance == null)
        {
            instance = this;
        }
        
    }
   

    private void OnEnable()
    {
        StartCoroutine(SelectAfterOneFrame());
     
    }


    IEnumerator SelectAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(card[0]);
    }
   
}
