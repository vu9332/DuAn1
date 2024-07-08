using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    public void GetKnockBack(Transform damageSource,float knockBackThurust)
    {
        gettingKnockedBack= true;
        Vector2 different = (transform.position - damageSource.position).normalized * knockBackThurust * rb.mass;
        StartCoroutine(KnockRoutine());
    }
    IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
