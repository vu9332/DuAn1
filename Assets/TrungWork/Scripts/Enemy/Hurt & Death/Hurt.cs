using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    private EnemyAIHealth enemyAIHealth;
    private Animator anim;
    private void Awake()
    {
        enemyAIHealth=GetComponent<EnemyAIHealth>();
        anim= GetComponent<Animator>();
    }
    void Update()
    {
        if (enemyAIHealth.isHurting)
        {
            anim.SetTrigger("Hurt");
            enemyAIHealth.isHurting = false;
        }
    }
}
