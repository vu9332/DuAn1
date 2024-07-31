    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int currentLevel { get { return playerData.playerLevel ; } set { playerData.playerLevel = value; } }
    [SerializeField] public float experiencePoints { get { return playerData.playerExp; } set { playerData.playerExp = value; } }
    [SerializeField] private int pointsPerLevel; 
  
    // Update is called once per frame
    void Update()
    {
        if (experiencePoints >= pointsPerLevel)
        {
            LevelUp();
        }
        CheckUnlockSkills(currentLevel);
    }
    void LevelUp()
    {
        currentLevel++;
        experiencePoints = 0;
       
    }
    void CheckUnlockSkills (int Level)
    {
        if(Level==2)
        {
            SkillManager.Instance.UnLockSkill("Skill 1");        
        }
       else if(Level==3)
        {
            SkillManager.Instance.UnLockSkill("Skill 2");
        }
       else if(Level==4)
        {
            SkillManager.Instance.UnLockSkill("Skill 3");
        }

    }
    public void levelUp()
    {
        experiencePoints += 100;
    }    
}
