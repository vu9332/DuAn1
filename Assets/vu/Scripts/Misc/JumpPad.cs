using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*bounce,ForceMode2D.Impulse);
        }
    }
}
