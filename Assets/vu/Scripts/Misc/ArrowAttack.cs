using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAttack: MonoBehaviour
{
    
    [SerializeField] private float arrowSpeed;
    private float arrowDamage;
    [SerializeField] private float timeDestroy;
    [SerializeField] private SkillData skillData;
    private Vector2 arrowDir;
    void Start()
    {
      
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(arrowDir.x * arrowSpeed, 0), ForceMode2D.Impulse);
        arrowDamage += skillData.damage + PlayerCombat.Instance.playerDamage; Debug.Log(arrowDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector2 GetDirectionArrowAttack( Vector2 dir )
    {       
        return arrowDir= dir;      
    }
    public float GetArrowDamage(float damage)
    {
        return arrowDamage= damage;
    }    
    private void OnTriggerEnter2D(Collider2D othe)
    {
        if( othe.gameObject.GetComponent<Enemy>() )
        {
          Enemy ene=othe.gameObject.GetComponent<Enemy>();
            ene.TakeDamage(arrowDamage);
            Destroy(this.gameObject, timeDestroy);
        }
    }
}
