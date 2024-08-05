using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFall : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = pointA.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, targetPosition) < 0.1f )
        {
            if(transform.position == pointA.position)
            {
                targetPosition=pointB.position;
            }
            else
            {
                targetPosition=pointA.position;
            }
        }
    }


}
