using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBitePlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.GetComponent<Player>())
        {
            Debug.Log("Player đã bị cắn!");
            FlyingEyes fe = GameObject.FindAnyObjectByType<FlyingEyes>().GetComponent<FlyingEyes>();
        }
    }
}
