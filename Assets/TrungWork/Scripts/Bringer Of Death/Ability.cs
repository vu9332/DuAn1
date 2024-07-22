using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private float distance;
    private Animator animator;
    public bool canUseAbility = false;
    private Transform playerPos;
    private Demon demon;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerPos=FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
        demon = GetComponent<Demon>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position,playerPos.position) > distance)
        {
            canUseAbility = true;
            demon.CanUseAbility = false;
        }
        else
        {
            canUseAbility = false;
            demon.CanUseAbility = true;
        }
        animator.SetBool(AnimationStrings.isAbility, canUseAbility);
    }
}
