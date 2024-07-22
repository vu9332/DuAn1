using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private Vector2 offSet;
    private void Awake()
    {
        playerPos=FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
    }
    [SerializeField] private GameObject HitSpell;
    void Hit()
    {
        Instantiate(HitSpell,(Vector2)playerPos.position + offSet, Quaternion.identity);
    }
}
