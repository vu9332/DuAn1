using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTrap : MonoBehaviour
{
    [SerializeField] private float speedRoll = 5f;
    [SerializeField] private float speedmove = 1f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Vector3 targetPoint;
    private void Start()
    {
        targetPoint = pointA.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,targetPoint,speedmove * Time.deltaTime);
        if(Vector3.Distance(transform.position,targetPoint)  < 0.1f )
        {
            if(transform.position == pointA.position)
            {
                targetPoint = pointB.position;
            }
            else
            { 
                targetPoint = pointA.position;
            }
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, speedRoll*360*Time.fixedDeltaTime);
    }
}
