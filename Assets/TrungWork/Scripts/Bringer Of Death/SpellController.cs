using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] private GameObject HitSpell;
    private void Start()
    {
        HitSpell.SetActive(false);
    }
    void Hit()
    {
        HitSpell.SetActive(true);
    }
    void CantHit()
    {
        HitSpell.SetActive(false);
    }
}
