using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        //Nếu kiếm Enemy trúng Player thì trừ máu Player
        if (enemy.gameObject.GetComponent<Player>())
        {
            Debug.Log("Đã va chạm Player");
        }
    }
}
