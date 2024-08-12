using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private float duration;
    [SerializeField] private Transform endPos;
    void Start()
    {

        creditText.rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
        creditText.rectTransform.DOAnchorPosY(endPos.position.y, duration, false).SetEase(Ease.Linear).OnComplete(() => SceneManager.LoadScene(2));
    }

   
}
