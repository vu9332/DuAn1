using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshProManager : MonoBehaviour
{
    public Color color;
    public TextMeshProUGUI textHello;
    void Start()
    {
        textHello.color = color;
    }
}
