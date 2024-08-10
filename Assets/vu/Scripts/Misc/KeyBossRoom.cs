using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBossRoom : MonoBehaviour
{
    [SerializeField] private PlayerData newData;

 
    private void Start()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>()!=null)
        {
            //  Player_Abilities.Instance.isGetKey = true;
            //SpriteRenderer im = this.gameObject.GetComponent<SpriteRenderer>();
           // im.DOFade(0f, 2);
        }
       
    }
}
