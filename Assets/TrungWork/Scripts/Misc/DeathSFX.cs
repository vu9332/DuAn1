using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSFX : MonoBehaviour
{
    [SerializeField] private float time;
    private ParticleSystem ps;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (gameObject.GetComponent<DeathSFX>())
        {
            Destroy(gameObject, time);
        }
    }
}
