    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    [SerializeField] private int experiencePoints;
    [SerializeField] private int pointsPerLevel; 
  
    // Update is called once per frame
    void Update()
    {
        if (experiencePoints >= pointsPerLevel)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        currentLevel++;
        experiencePoints = 0;
        CheckUnlockSkills(currentLevel);    
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
}
