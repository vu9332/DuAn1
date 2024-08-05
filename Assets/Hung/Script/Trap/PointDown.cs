using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDown : MonoBehaviour
{
    [SerializeField] private GameObject trapCircle;
    [SerializeField] private Transform pointDrop;
    [SerializeField] private float delayTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropTrap(delayTime));
    }
     

    private IEnumerator DropTrap(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Vector2 position = pointDrop.transform.position;
            Instantiate(trapCircle, position, Quaternion.identity);            
        }
        
    }
    
}
