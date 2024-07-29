using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOfBlackness : MonoBehaviour,IBossSkill
{
    [SerializeField] private float distance;
    private Transform playerPosition;
    public float damage { get; set; }
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerPosition = FindAnyObjectByType<PlayerController>().GetComponent<Transform>();  
    }
    void Update()
    {
        if (Vector2.Distance(transform.position,playerPosition.position) >= distance)
        {
            PlayAbility();
        }
        else
        {
            StopAbility();
        }
    }
    public void PlayAbility()
    {
        animator.SetBool(AnimationBoss.canUseAbility, true);
    }
    public void StopAbility()
    {
        animator.SetBool(AnimationBoss.canUseAbility, false);
    }
    void SoundBlackness()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_bringer_blackness);
    }
    void SoundLowAttackBringer()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_low_pl_attack6);
    }
    void SoundBringerHurt()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_bringer_hurt);
    }
}
