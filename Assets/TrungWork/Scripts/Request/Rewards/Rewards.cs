using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [Header("----------------List of all Rewards Object-------------")]

    public GameObject chest;
    public GameObject currency;


    public static Rewards rewardInstance;
    private void Awake()
    {
        if(rewardInstance == null)
        {
            rewardInstance = this;
        }
    }
    //Nhận thưởng
    public void GiveRewardToPlayer(GameObject rewards,Transform pos,int amount)
    {
        for(int i=0; i<amount;i++)
        {
            Instantiate(rewards, pos.position, Quaternion.identity);
        }
    }

    //Tăng kinh nghiệm
    public float GiveExperienceToPlayer(float playerExperiencePoints,int amountExeriencesRecieved)
    {
        playerExperiencePoints += amountExeriencesRecieved;
        return playerExperiencePoints;
    }
}
