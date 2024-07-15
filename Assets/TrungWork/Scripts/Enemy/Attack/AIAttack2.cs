using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIAttack2 : MonoBehaviour
{
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private Transform posShoot;
    private EnemyAIFindPlayer enemyAIFindPlayer;
    private Animator anim;
    private void Awake()
    {
        enemyAIFindPlayer = GetComponent<EnemyAIFindPlayer>();
        anim= GetComponent<Animator>();
    }
    private void Update()
    {
        if (enemyAIFindPlayer.canAttack)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.SetTrigger("Walking");
        }
    }
    void Shoot()
    {
        Instantiate(shootPrefab, posShoot.position,Quaternion.identity);
    }
}
