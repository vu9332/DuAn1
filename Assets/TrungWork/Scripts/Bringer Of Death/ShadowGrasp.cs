using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGrasp : MonoBehaviour
{
    [SerializeField] private float damage;
    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<PlayerController>())
        {
            if (player!= null)
            {
                Debug.Log("Nhan vat da bi trung skill!");
            }
        }
    }
}
