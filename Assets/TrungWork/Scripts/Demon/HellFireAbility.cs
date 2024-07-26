using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Boss;

public class HellFireAbility : MonoBehaviour,IBossSkill
{
    [SerializeField] private GameObject fireSplashPrefab;
    [SerializeField] private Transform fireSplashPosition;
    [SerializeField] private float distance;
    public float damage { get; set; }
    private Animator animator;
    private Transform playerPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerPosition=FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
    }
    public void PlayAbility()
    {
        FlipDirectiion();
        animator.SetBool(AnimationBoss.canUseAbility, true);
    }
    public void StopAbility()
    {
        animator.SetBool(AnimationBoss.canUseAbility, false);
    }
    public void HellFireMiddle()
    {
        Instantiate(fireSplashPrefab, fireSplashPosition.position,Quaternion.identity);
    }
    public void FlipDirectiion()
    {
        if (transform.position.x < playerPosition.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x > playerPosition.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
