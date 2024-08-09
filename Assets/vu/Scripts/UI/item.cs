using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class item : MonoBehaviour
{

    [SerializeField] ShopDataBase itemData;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelText2;
    [SerializeField] TextMeshProUGUI priceText;

    [Header("Button")]
    [SerializeField] private float scaleAmount;
    [SerializeField] private float duration;

    [Header("Slider")]
    [SerializeField] public Slider statusBar;
    [SerializeField] private float numberOfParts=3;
    float partValue;
   // public float coutSlideValue = 0;

    private void Start()
    {
       
         partValue = 1 / numberOfParts;
    }
    private void Update()
    {
        //CheckValue();
        AutoUpdateText();
    }
    void AutoUpdateText()
    {
        
        priceText.text = itemData.price.ToString();
        levelText2.text = "LV:" + itemData.LevePassiveSkill;
        levelText.text = "LV:" + itemData.LevePassiveSkill;
    }
    private void OnEnable()
    {
        statusBar.value = itemData.saveSlideValue;
        levelText.text = "LV:" + itemData.LevePassiveSkill;
        levelText2.text = "LV:" + itemData.LevePassiveSkill;
        priceText.text=itemData.price.ToString();
    }
    public void Buying(ref float data)
    {
        if (itemData.NumberOfPurchases<=itemData.maxPurchaseLimit&& itemData.playerData.playerCoin >= itemData.price)
        {
            this.gameObject.GetComponent<Button>().enabled = false;
            data += itemData.howMuchMore;
            PlayerHealth.Instance.UseCoin(itemData.price);
            itemData.NumberOfPurchases++;
            itemData.price += 50;
            itemData.countPress++;
            StartCoroutine(UpdateSliderInParts());
            itemData.saveSlideValue += partValue;

        }
    }    
   public void CheckValue()
    {
        if (itemData.countPress >= numberOfParts&& itemData.NumberOfPurchases != itemData.maxPurchaseLimit)
        {
            StartCoroutine(DecreaseSliderValue());
            //StartCoroutine(UpdateSliderInParts());
            statusBar.transform.DOShakePosition(.2f, 20f);
            itemData.LevePassiveSkill++;
            itemData.saveSlideValue = 0;

        }
        this.transform.DOScale(scaleAmount, duration).OnComplete(() => this.transform.DOScale(1f, duration));   
    }
    IEnumerator DecreaseSliderValue()
    {

            itemData.countPress = 0;
            this.gameObject.GetComponent<Button>().enabled = false;
            yield return new WaitForSeconds(.8f);    
            float elapsed = 0f;
            float duration = .4f;
            while (elapsed < duration)
            {

                float value = Mathf.Lerp(1f, 0f, elapsed / duration);
                statusBar.value = value;
                elapsed += Time.deltaTime;
                yield return null;
            }
            statusBar.value = 0f;
            this.gameObject.GetComponent<Button>().enabled = true;
                
    }
   public  IEnumerator UpdateSliderInParts()
    { 
        float startValue = statusBar.value; 
        float endValue =Mathf.Clamp(startValue+ partValue, 0f,1f);
        float elapsed = 0f;
        float durationOfParts = .2f;
      
        // this.gameObject.GetComponent<Image>().enabled = false;
        while (elapsed < durationOfParts)
        {
            float value= Mathf.Lerp(startValue,endValue,elapsed / durationOfParts);
            statusBar.value=value;
            elapsed += Time.deltaTime;
            yield return null;
        }
       statusBar.value=endValue;   
        this.gameObject.GetComponent<Button>().enabled = true;
        //this.gameObject.GetComponent<Image>().enabled = true;

    }
  
}
