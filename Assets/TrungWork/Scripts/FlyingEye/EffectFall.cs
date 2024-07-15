using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFall : MonoBehaviour
{
    [SerializeField] private GameObject Effect_Fall;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private Transform groundPointUp;
    [SerializeField] private Transform groundPointDown;
    [SerializeField] private float radius;
    public Vector3 offSet;
    [Header("Sound")]
    [SerializeField] private AudioClip hitGround;
    public bool isTouchingGround;
    private void Awake()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position +offSet, radius);
    }
    public void Effectfall()
    {
        isTouchingGround = Physics2D.OverlapCircle(transform.position + offSet, radius, layerGround);
        if (isTouchingGround && FlyingEyes.Instance.isTouchingUp)
        {
            Instantiate(Effect_Fall, groundPointUp.position, Quaternion.Euler(180, 0, 0));
            SoundFXManagement.Instance.PlaySoundFXClip(hitGround, transform, hitGround.length);
        }
        if (isTouchingGround && FlyingEyes.Instance.isTouchingDown)
        {
            Instantiate(Effect_Fall, groundPointDown.position, Quaternion.Euler(0, 0, 0));
            SoundFXManagement.Instance.PlaySoundFXClip(hitGround, transform, hitGround.length);
        }
    }
}
