using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefabs;
    private bool isAppear = false;
    private void Start()
    {
        enemyPrefabs.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (!isAppear)
            {
                enemyPrefabs.SetActive(true);
                isAppear = true;
            }
            if (isAppear)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
