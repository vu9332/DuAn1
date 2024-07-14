using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIType1 : MonoBehaviour
{
    [SerializeField] private Transform posLeft;
    [SerializeField] private Transform posRight;
    [SerializeField] private float speed;
    private Vector2 posTarget;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        posTarget = posRight.position;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, posLeft.position) <.1f)
        {
            posTarget=posRight.position;
        }
        if (Vector2.Distance(transform.position, posRight.position) < .1f)
        {
            posTarget = posLeft.position;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (posTarget - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime);
    }
}
