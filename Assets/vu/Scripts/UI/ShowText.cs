using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject boxText;

    // Coroutine textBlink;

    private void Start()
    {
        boxText.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            boxText.SetActive(true);
            TextBlink();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            boxText.SetActive(false);
            this.gameObject.SetActive(false);
      
        }
    }
    void TextBlink()
    {
       
        text.DOFade(0, .7f).SetLoops(-1, LoopType.Yoyo);
    }

}
