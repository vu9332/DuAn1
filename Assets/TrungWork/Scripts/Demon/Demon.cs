using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Boss
{
    Animator myAnimator;
    protected override void Awake()
    {
        myAnimator = GetComponent<Animator>();
        base.Awake();
    }
    void Update()
    {
        if(OpenBossRoom.Instance.IsBossWakeUp)
        {
            myAnimator.SetBool("WakeUp", OpenBossRoom.Instance.IsBossWakeUp);
            FindPlayer();
        }
    }
    private void FindPlayer()
    {
        base.FindCollider();
    }
    private void MoveMent()
    {
        base.Move();
    }
    void PlaySFXWhenTheBossAttackByBloodKnives()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_demon_knives);
    }
    
}
