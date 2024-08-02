    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }
   
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int currentLevel { get { return playerData.playerLevel ; } set { playerData.playerLevel = value; } }
    [SerializeField] public float experiencePoints { get { return playerData.playerExp; } set { playerData.playerExp = value; } }
    [SerializeField] private int pointsPerLevel;



    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }
    void Update()
    {
        if (experiencePoints >= pointsPerLevel)
        {
            LevelUp();
        }
        //CheckUnlockSkills(currentLevel);
    }
    void LevelUp()
    {
        currentLevel++;
        experiencePoints = 0;

        }
        //void CheckUnlockSkills (int Level)
        //{
        //    if(Level==2)
        //    {
        //        UnLockSkill("Skill 1");        
        //    }
        //   else if(Level==3)
        //    {
        //       UnLockSkill("Skill 2");
        //    }
        //   else if(Level==4)
        //    {
        //      UnLockSkill("Skill 3");
        //    }

        //}
        //public void levelUp()
        //{
        //    experiencePoints += 100;
        //}

    }
