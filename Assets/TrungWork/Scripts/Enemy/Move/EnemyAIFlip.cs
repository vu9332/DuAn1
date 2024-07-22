using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIFlip : MonoBehaviour
{
    private Transform playerPos;
    public float dirLineCast;
    private void Awake()
    {
        playerPos=FindAnyObjectByType<PlayerController>().GetComponent<Transform>().transform;
    }
    public void Flip()
    {
        if (playerPos.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            dirLineCast = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            dirLineCast = 1;
        }
    }
}
