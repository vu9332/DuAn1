using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIFindPlayer : MonoBehaviour
{
    [SerializeField] private float distanceToAttackPlayer;
    private EnemyAIType1 enemyAIType1;
    public bool canAttack;
    private void Awake()
    {
        enemyAIType1 = GetComponent<EnemyAIType1>();
    }

    private void FixedUpdate()
    {
        Vector2 endPos = transform.position + Vector3.right * distanceToAttackPlayer * enemyAIType1.dirLineCast;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                canAttack = true;
                Debug.DrawLine(transform.position, endPos, Color.green);
            }
            else
            {
                canAttack = false;
                Debug.DrawLine(transform.position, endPos, Color.red);
            }
        }
        else
        {
            canAttack = false;
        }
    }
}
