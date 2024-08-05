using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public static MessageManager istance;

    [SerializeField] private TextMeshProUGUI messagerText;
    [SerializeField] private GameObject messagerPanel;

    private void Start()
    {
        if (istance == null)
        {
            istance = this;
        }
        messagerPanel.gameObject.SetActive(false);
    }
    private void GetText(string text)
    {
        messagerText.text = text;
    }
    public void StartPopUp(string text, float displayDuration)
    {
        GetText(text);
        messagerPanel.gameObject.SetActive(true);
        messagerText.gameObject.SetActive(true);
        float elapsed=displayDuration;
        elapsed-=Time.deltaTime;
        if (elapsed>=0)
        {
            messagerPanel.transform.localPosition = new Vector3(0, 200, 0);

            messagerPanel.transform.DOLocalMoveY(-400, .5f).SetEase(Ease.OutBounce);
            Fade(displayDuration);
        }    
          
    }
 
    private void Fade(float displayDuration)
    {
        DOVirtual.DelayedCall(displayDuration, () =>
        {
            Image image = messagerPanel.GetComponent<Image>();

            image.DOFade(0,.2f).OnComplete(() =>
            {

                messagerPanel.gameObject.SetActive(false);
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                messagerPanel.GetComponent<Image>().color = image.color;
            });
            messagerText.DOFade(0, .2f).OnComplete(() =>
            {
                messagerText.gameObject.SetActive(false);
                messagerText.color = new Color(messagerText.color.r, messagerText.color.g, messagerText.color.b, 1);

            });

        });
    }
}
