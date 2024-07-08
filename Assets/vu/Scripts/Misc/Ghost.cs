using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    [SerializeField] float ghostDelay;
     float ghostDelaySeconds;
    public bool makeGhost;

    void Start()
    {
        ghostDelaySeconds=ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite=GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale=this.transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite=currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, .5f);
            }
        }
      
        
    }
}
