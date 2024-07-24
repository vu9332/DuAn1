using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
   public bool gettingKneckBack {  get; private set; }

    [SerializeField] private float knockBackTime;

    Rigidbody2D rb;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();    
    }
    public void GetKnockedBack(Transform dameSource, float knockbackThurst)
    {
        gettingKneckBack = true;
        Vector2 difference=(transform.position - dameSource.position).normalized*knockbackThurst*rb.mass;
        rb.AddForce(difference,ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());

    }
    IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero; 
        gettingKneckBack= false;
    }    
   
}
