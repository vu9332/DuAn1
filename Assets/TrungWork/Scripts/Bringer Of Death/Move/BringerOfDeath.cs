using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
}