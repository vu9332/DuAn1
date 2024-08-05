using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.02f;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        distance += Time.deltaTime*speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
