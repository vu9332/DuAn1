using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps= GetComponent<ParticleSystem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (ps && !ps.IsAlive())
        {
            DestroySelf();
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
