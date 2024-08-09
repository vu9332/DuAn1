using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour
{
    public static Level levelInstance;
    [SerializeField] private GameObject experiencesBar;
    [SerializeField] private Image experiences;
    [SerializeField] private PlayerData pl;
    [SerializeField] private TextMeshProUGUI playerLevel;
    [SerializeField] private ex_Level exLevel;
    private int maxLevel;
    private float rateExperiencesUpLevel;
    private float experiencesDefault;
    private float experiencesPlayerNeedToUpLevel;
    private bool isMaxLevel;
    private void Start()
    {
        experiencesDefault = 100;
        maxLevel=pl.playeMaxLevel;
        rateExperiencesUpLevel= 1 + (float)exLevel.rateExperiencesUpLevel/100;
        if (pl.playerLevel >= maxLevel)
        {
            isMaxLevel = true;
        }
        levelInstance = this;
        experiencesPlayerNeedToUpLevel = experiencesDefault*Mathf.Pow(rateExperiencesUpLevel,pl.playerLevel);
        experiencesBar.SetActive(true);
    }
    private void Update()
    {
        if (!isMaxLevel)
        {
            experiences.fillAmount = (float)pl.playerExp / experiencesPlayerNeedToUpLevel;
            playerLevel.text = "Lv. " + pl.playerLevel.ToString();
            if (pl.playerExp >= experiencesPlayerNeedToUpLevel)
            {
                pl.playerLevel = ++pl.playerLevel;
                experiencesPlayerNeedToUpLevel = experiencesDefault * Mathf.Pow(rateExperiencesUpLevel, pl.playerLevel);
                pl.playerExp = 0;
            }
        }
        else
        {
            playerLevel.text = "MAX Lv. " + maxLevel.ToString();
            experiencesBar.SetActive(false);
        }
        if (pl.playerLevel >= maxLevel)
        {
            isMaxLevel = true;
        }
    }
}
