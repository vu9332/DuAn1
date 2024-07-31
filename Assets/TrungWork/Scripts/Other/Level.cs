using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour
{
    public static Level levelInstance;
    [SerializeField] private Image experiences;
    [SerializeField] private PlayerData pl;
    [SerializeField] private TextMeshProUGUI playerLevel;
    private float experiencesPlayerNeedToUpLevel;
    private void Start()
    {
        levelInstance = this;
        experiencesPlayerNeedToUpLevel = 100;
    }
    private void Update()
    {
        experiences.fillAmount = (float)pl.playerExp / experiencesPlayerNeedToUpLevel;
        playerLevel.text = "LEVEL "+ pl.playerLevel.ToString();
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
