using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private float duration;
    void Start()
    {

        creditText.rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
        creditText.rectTransform.DOAnchorPosY(Screen.height, duration, false).SetEase(Ease.Linear);
    }

   
}
