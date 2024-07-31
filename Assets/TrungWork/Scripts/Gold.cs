using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            playerData.playerCoin = ++playerData.playerCoin;
            Destroy(gameObject);
        }
    }
}
