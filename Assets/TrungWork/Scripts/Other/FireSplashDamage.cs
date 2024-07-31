using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSplashDamage : FireSplash
{
    [SerializeField] private bossDemon bossDemon;
    protected override void Awake()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_fire_splash);
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        damage = bossDemon.damageSkill_1;
    }
    private void Update()
    {
        base.Move();
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<PlayerController>() != null)
        {
            Destroy(gameObject);
            PlayerHealth player=hit.GetComponent<PlayerHealth>();
            player.TakeDamage(damage);
        }
        if (isTouchedWall)
        {
            Destroy(gameObject);
        }
    }
}
