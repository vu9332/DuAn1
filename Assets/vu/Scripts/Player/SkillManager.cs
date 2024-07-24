using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance {  get; set; }


    [SerializeField]
    private bool _isSkillOneUnlock = false;   
    public bool IsSkillOneUnlock { get { return _isSkillOneUnlock; } set { _isSkillOneUnlock = value; CardSelectionManager.instance.card[0].gameObject.SetActive(!value); } }


    [SerializeField]
    private bool _isSkillTwoUnlock = false;
    public bool IsSkillTwoUnlock { get { return _isSkillTwoUnlock; } set { _isSkillTwoUnlock = value; CardSelectionManager.instance.card[1].gameObject.SetActive(!value); } }


    [SerializeField]
    private bool _isSkillThreeUnlock = false;
    public bool IsSkillThreeUnlock { get { return _isSkillThreeUnlock; } set { _isSkillThreeUnlock = value; CardSelectionManager.instance.card[2].gameObject.SetActive(!value); } }

  //  [SerializeField] public List<ISkill> skillList=new List<ISkill>();


    private void Awake()
    {
      // skillList = GetComponents<ISkill>().ToList();
     
    }
    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

    }
    // Update is called once per frame
    public void UnLockSkill( string skillName )
    {
        switch (skillName)
        {
            case "Skill 1":
                IsSkillOneUnlock = true;             
                break;
            case "Skill 2":
                IsSkillTwoUnlock = true;              
                break;
            case "Skill 3":
               IsSkillThreeUnlock = true;              
                break;
        }

    }

    void CheckSkillIfOpened()
    {

    }    


}



