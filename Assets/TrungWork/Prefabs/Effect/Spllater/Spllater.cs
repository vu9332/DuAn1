using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spllater : MonoBehaviour
{
    private SpriteFade sf;
    private void Awake()
    {
        sf = GetComponent<SpriteFade>();
    }
    private void Start()
    {
        //Destroy(gameObject,2f);
        StartCoroutine(sf.SlowFadeRoutine());
    }
}
