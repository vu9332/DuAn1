using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private float restoreDefaultMaterial;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        defaultMaterial= GetComponent<Material>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        defaultMaterial=spriteRenderer.material;
    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material=whiteMaterial;
        yield return new WaitForSeconds(restoreDefaultMaterial);
        spriteRenderer.material=defaultMaterial;
    }
}
