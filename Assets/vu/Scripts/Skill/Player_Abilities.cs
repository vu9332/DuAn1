    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_Abilities : MonoBehaviour
{
    public static Player_Abilities Instance {  get;  set; }   

    public delegate void SkillAbilities();
    public static SkillAbilities Skill1AbilityCoolDown;
    public static SkillAbilities Skill2AbilityCoolDown;
    public static SkillAbilities Skill3AbilityCoolDown;
    public static SkillAbilities DashAbilityCoolDown;
    [SerializeField] Image skill1Image;
    [SerializeField] SkillData skill1Data;
    [SerializeField] Image skill2Image;
    [SerializeField] SkillData skill2Data; 
    [SerializeField] Image skill3Image;
    [SerializeField] SkillData skill3Data; 
    [SerializeField] Image DashImage;
    [SerializeField] PlayerData playerData;
    void Start()
    {
        //if (SkillManager.Instance.IsSkillOneUnlock)
            skill1Image.fillAmount = 0; 
        //if (SkillManager.Instance.IsSkillTwoUnlock)
            skill2Image.fillAmount = 0; 
            skill3Image.fillAmount = 0; 
        DashImage.fillAmount = 0;
        //if (SkillManager.Instance.IsSkillOneUnlock)
        //    skill1Image.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Ability1();
    }
    public static void skill1AbilityCoolDown()
    {
      Skill1AbilityCoolDown?.Invoke();
    } 
    public static void skill2AbilityCoolDown()
    {
      Skill2AbilityCoolDown?.Invoke();
    }  
    public static void skill3AbilityCoolDown()
    {
      Skill3AbilityCoolDown?.Invoke();
    }   
    public static void dashAbilityCoolDown()
    {
      DashAbilityCoolDown?.Invoke();
    }    
    private void OnEnable()
    {
        Skill1AbilityCoolDown += Ability1;
        Skill2AbilityCoolDown += Ability2;
        Skill3AbilityCoolDown += Ability3;
        DashAbilityCoolDown += Dash;
    }
    private void OnDisable()
    {
        Skill1AbilityCoolDown -= Ability1;
        Skill2AbilityCoolDown -= Ability2;
        Skill3AbilityCoolDown -= Ability3;
        DashAbilityCoolDown -= Dash;
    }
    void Ability1()
    {
        skill1Image.fillAmount = 1;     
         StartCoroutine(AbilityRoutine(skill1Image,skill1Data));      
    }

    void Ability2()
    {
         skill2Image.fillAmount = 1;     
         StartCoroutine(AbilityRoutine(skill2Image,skill2Data));      
    }
  void Ability3()
    {
         skill3Image.fillAmount = 1;     
         StartCoroutine(AbilityRoutine(skill3Image,skill3Data));      
    } 
    void Dash()
    {
         DashImage.fillAmount = 1;     
         StartCoroutine(DashRoutine(DashImage,playerData));      
    }
  
   IEnumerator AbilityRoutine(Image image,SkillData skData)
    {
        while(image.fillAmount>0)
        {
            yield return null;
            if (!skData._DoSkill)
            {

                image.fillAmount -= 1 / skData.coolDownTime * Time.deltaTime;
                if (image.fillAmount <= 0)
                {
                    image.fillAmount = 0;
                }
            }
        }    
    } 
  IEnumerator DashRoutine(Image image,PlayerData skData)
    {
        while(image.fillAmount>0)
        {
            yield return null;
            if (!skData.CanDash)
            {

                image.fillAmount -= 1 / skData.dashCD * Time.deltaTime;
                if (image.fillAmount <= 0)
                {
                    image.fillAmount = 0;
                }
            }
        }    
    } 
  
   

}
