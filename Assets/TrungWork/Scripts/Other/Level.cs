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
    [SerializeField] private int maxLevel;
    private float experiencesPlayerNeedToUpLevel;
    private bool isMaxLevel;
    private void Start()
    {
        if(pl.playerLevel >= maxLevel)
        {
            isMaxLevel = true;
        }
        levelInstance = this;
        experiencesPlayerNeedToUpLevel = 100;
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
                experiencesPlayerNeedToUpLevel += 100;
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
    public void UpLevelIfPlayerGotFull(float a)
    {
        if(pl.playerExp >= experiencesPlayerNeedToUpLevel)
        {
            pl.playerLevel = pl.playerLevel++;
        }
        experiencesPlayerNeedToUpLevel += 100;
    }
}
