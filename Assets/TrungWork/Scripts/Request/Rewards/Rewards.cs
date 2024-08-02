using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [Header("----------------List of all Rewards Object-------------")]

    public GameObject chest;
    public GameObject currency;


    public static Rewards rewardInstance;
    public bool isCoinFall=false;
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
        Vector2 defaultPosition=pos.position;
        isCoinFall = rewards == currency ? true : false;
        for(int i=0; i<amount;i++)
        {
            if (isCoinFall)
            {
                pos.position = new Vector2(pos.position.x+Random.Range(-4,4f),pos.position.y+5f);
            }
            Instantiate(rewards, pos.position, Quaternion.identity);
            pos.position = defaultPosition;
        }
    }
}
