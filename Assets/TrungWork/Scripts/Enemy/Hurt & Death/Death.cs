using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Death : MonoBehaviour
{
    private EnemyAIHealth enemyAIHealth;
    private Animator anim;
    private void Awake()
    {
        enemyAIHealth = GetComponent<EnemyAIHealth>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (enemyAIHealth.isDead && enemyAIHealth.isHurting)
        {
            anim.SetTrigger("Death");
        }
    }
}
