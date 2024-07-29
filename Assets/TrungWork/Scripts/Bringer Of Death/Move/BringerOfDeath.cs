using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : Boss
{
    protected override void Awake()
    {
        base.Awake();
    }
    void Update()
    {
        FindPlayer();
    }
    private void FindPlayer()
    {
        base.FindCollider();
    }
    void SFXBringerAttackBasicKnives()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_bringer_attack);
    }
}