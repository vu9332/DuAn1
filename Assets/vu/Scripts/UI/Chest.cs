using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour, IDamageAble
{
    [Header("key Instantiate")]
    [SerializeField] private float chestHealth;
    [SerializeField] private float keyJumpHeight;
    [SerializeField] private float keyJumpDoration;
    [SerializeField] private Transform keySpawnPoint;
    [SerializeField] private GameObject keyRoom;
    [Header("")]
    [SerializeField] private float time;
    [SerializeField] private bool isKey=false;
    private float timer = 0;
    public float health { get { return chestHealth; } set { chestHealth = value; } }
    
    public void Die()
    {
        if (!isKey) 
            StartCoroutine(JumpKey());
    }
    IEnumerator JumpKey()
    {
        GameObject go = Instantiate(keyRoom,keySpawnPoint.position,Quaternion.identity);
        go.transform.DOMoveY(go.transform.position.y+keyJumpHeight,keyJumpDoration).SetEase(Ease.OutCirc);
        isKey = true;
        timer = time;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (timer <= 0)
        {
          go.gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
           // go.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }    
    
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else if(health <= 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.Play("Chest");
            health = 0;
            Die();
        }
    }
}
