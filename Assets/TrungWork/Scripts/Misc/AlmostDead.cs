using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmostDead : MonoBehaviour
{
    [SerializeField] private Material redMaterial;
    [SerializeField] private float restoreDefaultMaterial;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        defaultMaterial = GetComponent<Material>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        defaultMaterial = spriteRenderer.material;
    }
    public IEnumerator FlashRoutine()
    {
        while(true)
        {
            spriteRenderer.material = redMaterial;
            yield return new WaitForSeconds(restoreDefaultMaterial);
            spriteRenderer.material = defaultMaterial;
            yield return new WaitForSeconds(restoreDefaultMaterial);
        }
    }
}
