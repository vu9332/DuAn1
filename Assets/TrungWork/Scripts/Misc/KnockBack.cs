using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime = .2f;
    public bool gettingKnockedBack { get; private set; }
    private Rigidbody2D rb;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    public void GetKnockBack(Transform damageSource,float knockBackThurust)
    {
        gettingKnockedBack= true;
        Vector2 different = (transform.position - damageSource.position).normalized * knockBackThurust * rb.mass;
        rb.AddForce(new Vector2(different.x,rb.velocity.y), ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }
    IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
