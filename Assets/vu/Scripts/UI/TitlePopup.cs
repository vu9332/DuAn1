using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitlePopup : MonoBehaviour
{
    public static TitlePopup instance;
    [SerializeField] private Image boxTitle;
    [SerializeField] private Image skullImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI titleContinue;
    [Header("Result box")]
    [SerializeField] private GameObject panelResult;


    void Start()
    {
        if (instance == null)
            instance = this;


        title.gameObject.SetActive(false);
        boxTitle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameoverText()
    {
        title.color=new Color(title.color.r,title.color.g,title.color.b,0);
        boxTitle.color=new Color(boxTitle.color.r, boxTitle.color.g, boxTitle.color.b,0);
        skullImage.color=new Color(skullImage.color.r, skullImage.color.g, skullImage.color.b,0);
        title.gameObject.SetActive(true);
        boxTitle.gameObject.SetActive(true);
        title.DOFade(1f, 3f);
        boxTitle.DOFade(1f, 2f);
        skullImage.DOFade(1f, 2f);
        BlinkText();
    }
    public void ShowDeathBox() => StartCoroutine(DeathBoard());
   IEnumerator DeathBoard()
    {      
        title.DOFade(0f, 1f);
        boxTitle.DOFade(0f, 1f).OnComplete(() => OpenDeathBox());
        skullImage.DOFade(0f, 1f);
        titleContinue.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        title.gameObject.SetActive(false);
        boxTitle.gameObject.SetActive(false);
    }
    private void OpenDeathBox()
    {
        panelResult.SetActive(true);
    }
    void BlinkText()
    {
        titleContinue.DOFade(0,.7f).SetLoops(-1,LoopType.Yoyo);

    }    
}
