using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject boxText;
    [SerializeField] private GameObject fade;

    // Coroutine textBlink;
    [SerializeField] private bool canFade;
    
 
    private void Start()
    {
        boxText.gameObject.SetActive(false);
        fade.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            boxText.SetActive(true);
            TextBlink();
            if (canFade)
            {
                fade.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            if(boxText.gameObject.activeSelf==true)
            {

                   boxText.SetActive(false);
            }

            if (canFade)
            {

                fade.SetActive(false);
            }
            
            //  wasJoin = true;
            // this.gameObject.SetActive(false);

        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<PlayerHealth>() != null && !wasJoin)
    //    {
    //       text.color=new Color(text.color.r, text.color.g, text.color.b,1);
    //        boxText.SetActive(true);
    //        TextBlink();

    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<PlayerHealth>() != null)
    //    {
    //        boxText.SetActive(false);

    //        text.DOKill();

    //    }
    //}
    void TextBlink()
    {
       
        text.DOFade(0, .7f).SetLoops(-1, LoopType.Yoyo);
    }

}
