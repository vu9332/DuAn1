using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Boss
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
