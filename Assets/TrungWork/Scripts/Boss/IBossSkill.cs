using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBossSkill
{
    public float damage { get; set; }
    public void PlayAbility();
    public void StopAbility();
}
