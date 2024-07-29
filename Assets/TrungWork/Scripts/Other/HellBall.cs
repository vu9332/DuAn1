using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBall : FireSplash
{
    [SerializeField] private bossDemon bossDemon;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        damage = bossDemon.damageSkill_2;
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
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            player.TakeDamage(damage);
        }
    }
}
