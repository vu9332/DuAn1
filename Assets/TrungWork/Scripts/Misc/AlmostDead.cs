using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmostDead : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timeSwitch;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public IEnumerator FlashRoutine()
    {
        while(true)
        {
            spriteRenderer.color = new Color(1, 1, 1);
            yield return new WaitForSeconds(timeSwitch);
            spriteRenderer.color = new Color(1, 0, 0);
            yield return new WaitForSeconds(timeSwitch);
        }
    }
}
