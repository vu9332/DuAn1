using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour,IDamageAble
{
    [SerializeField] private float damage;
    [SerializeField] private float knockBackThrust;
    private KnockBack knockBack;

    public float health { get; set; }
    public float currentHealth { get ; set ; }

    private void Awake()
    {
        knockBack= GetComponent<KnockBack>();
    }
    private void Start()
    {
        currentHealth = health;
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Da trung Player!");
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        Die();
    }
    //Nếu máu về 0
    public void Die()
    {
        if(currentHealth <= 0)
        {
            
        }
    }
}
