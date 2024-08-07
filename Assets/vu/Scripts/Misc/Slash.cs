using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{

    [SerializeField] float timeToDestroyObject;
    [SerializeField] float damageSlash;
    [SerializeField] private float slashSpeed;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private SkillData skillData;

    Rigidbody2D rb;
    Vector2 slashDirection;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        Vector2 slashDir = (PlayerController.Instance.IsFacingRight) ? Vector2.right : Vector2.left;
        if (slashDir != Vector2.right)
        {
            this.transform.localScale *= new Vector2(-1, 1);
        }
        rb.AddForce(slashDirection * slashSpeed, ForceMode2D.Impulse);
        damageSlash += skillData.damage;
    }
    public Vector2 SetSlashDirection(Vector2 dir)
    {
        return slashDirection = dir.normalized;
    }
    private void Update()
    {
        timeToDestroyObject-= Time.deltaTime;
        if (timeToDestroyObject <=0)
        {
            Destroy(this.gameObject);   
        }
    }
    public float GetSlashDamage(float damage)
    {
        return damageSlash = damage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damageSlash);
            Debug.Log("Chem trung");
            Destroy(this.gameObject,.05f);
            GameObject hitVFX=  Instantiate(particleOnHitPrefabVFX,other.transform.position,other. transform.rotation);
            Destroy(hitVFX,.1f);
        }
    }
}
