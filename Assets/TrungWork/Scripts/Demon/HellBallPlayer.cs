using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBallPlayer : MonoBehaviour
{
    [SerializeField] private GameObject hellBallPrefab;
    [SerializeField] Transform hellBallPosition;
    public void HellBall()
    {
        Instantiate(hellBallPrefab,hellBallPosition.position,Quaternion.identity);
    }
}
