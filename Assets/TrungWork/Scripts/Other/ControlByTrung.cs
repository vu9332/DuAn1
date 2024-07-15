using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlByTrung : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject bossPrefabs;
    [SerializeField] private Transform posPrefabs;
    private Collider2D coll;
    private bool bossIsAppear;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void Start()
    {
        StartCoroutine(BossAppear(bossPrefabs, posPrefabs));
    }
    IEnumerator BossAppear(GameObject prefabs, Transform pos)
    {
        while (true)
        {
            if (coll.IsTouchingLayers(groundLayer))
            {
                bossIsAppear = true;
                break;
            }
        }
        if (bossIsAppear)
        {
            Instantiate(bossPrefabs, posPrefabs.position, Quaternion.identity);
        }
        yield return null;
    }
}
