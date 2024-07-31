using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private Vector3 moveSpeed = new Vector3(0, 100, 0);
    private float timeToFade = 1.0f;
    private float timeElapsed = 0f;
    RectTransform textHealth;
    TextMeshProUGUI textDamage;
    private Color startColor =new Color(1,0,0);
    private void Awake()
    {
        textHealth = GetComponent<RectTransform>();
        textDamage= GetComponent<TextMeshProUGUI>();
        textDamage.color = startColor;
    }
    private void Update()
    {
        textHealth.position += moveSpeed * Time.deltaTime;
        timeElapsed+=Time.deltaTime;
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha=startColor.a*(1- (timeElapsed/timeToFade));
            textDamage.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject,5f);
        }
    }
}
