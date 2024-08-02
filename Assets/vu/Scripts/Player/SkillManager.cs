using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance {  get; set; }

    [SerializeField] private PlayerData playerData;



    [SerializeField]
    public bool IsSkillOneUnlock
    {
        get
        {
            return playerData._isSkillOneUnlock;
        }
        set
        {
            playerData._isSkillOneUnlock = value;
            //if (CardSelectionManager.instance.card[0].gameObject != null)
            //  CardSelectionManager.instance.card[0].gameObject.SetActive(!value);
            Abilities.transform.GetChild(0).gameObject.SetActive(value);
        }
    }





    [SerializeField]
    public bool IsSkillTwoUnlock
    {
        get
        {
            return playerData._isSkillTwoUnlock;
        }
        set
        {
            playerData._isSkillTwoUnlock = value;
            //if (CardSelectionManager.instance.card[1].gameObject != null)
            //    CardSelectionManager.instance.card[1].gameObject.SetActive(!value);
            Abilities.transform.GetChild(1).gameObject.SetActive(value);
        }
    }



    [SerializeField]
    public bool IsSkillThreeUnlock
    {
        get
        {
            return playerData._isSkillThreeUnlock;
        }
        set
        {
            playerData._isSkillThreeUnlock = value;
            //if (CardSelectionManager.instance.card[2].gameObject!=null)
            //     CardSelectionManager.instance.card[2].gameObject.SetActive(!value);
            Abilities.transform.GetChild(2).gameObject.SetActive(value);
        }
    }

    [SerializeField] GameObject Abilities;
    private void Awake()
    {
     
     
    }
    void Start()
    {

        UpdateIcon();
        if (Instance == null)
        {
            Instance = this;
        }
    }

   public void UpdateIcon()
    {
        IsSkillThreeUnlock = playerData._isSkillThreeUnlock;
        IsSkillTwoUnlock = playerData._isSkillTwoUnlock;
        IsSkillOneUnlock = playerData._isSkillOneUnlock;
    }    
    private void Update()
    {
        
    }
  
    // Update is called once per frame
    public void UnLockSkill( string skillName )
    {
        switch (skillName)
        {
            case "Skill 1":
                IsSkillOneUnlock = true;
                UpdateIcon();
                break;
            case "Skill 2":
                IsSkillTwoUnlock = true;
                UpdateIcon();
                break;
            case "Skill 3":
                IsSkillThreeUnlock = true;
                UpdateIcon();
                break;
        }

    }

 


}



