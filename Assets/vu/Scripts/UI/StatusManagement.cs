using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StatusManagement : MonoBehaviour
{
    public delegate void PassiveSkills();
    public static PassiveSkills OnHealth;
    public static PassiveSkills OnStamina;
    public static PassiveSkills OnDamage;
    public static PassiveSkills HideBoard;
    public static PassiveSkills ShowBoard;
    public static PassiveSkills ShowUI;
    public static PassiveSkills HideUI;

   // [SerializeField] private PlayerData playerData;
    [SerializeField] private item item1;

   // [SerializeField] private ShopDataBase itemData2;
    [SerializeField] private item item2;

    //[SerializeField] private ShopDataBase itemData3;
    [SerializeField] private item item3;

    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private GameObject passiveSkillsBoard;
    [SerializeField] private GameObject SkillsBar;
    [SerializeField] private GameObject SliderPlayer;
    [Space(10)]
    [SerializeField] private AudioClip clickAudio;
    private void Update()
    {
        AutoUpdateValue();
    
    }
    void AutoUpdateValue()
    {
       

      
    }
    private void OnEnable()
    {
     

        OnStamina += UpdateStaminaBar;
        OnDamage += UpdateDamageBar;
        OnHealth += UpdateHealthBar;

        HideBoard += HidePassiveSkillsBoard;
        ShowBoard += ShowPassiveSkillsBoard;

        HideUI += HideUIFunction;
        ShowUI += ShowUIFunction;


    }
    private void OnDisable()
    {
      //  passiveSkillsBoard.transform.DOMove(new Vector3(-700, 0, 0), .1f);

        OnStamina -= UpdateStaminaBar;
        OnDamage -= UpdateDamageBar;
        OnHealth -= UpdateHealthBar;

        HideBoard -= HidePassiveSkillsBoard;
        ShowBoard -= ShowPassiveSkillsBoard;

        HideUI -= HideUIFunction;
        ShowUI -= ShowUIFunction;
    }
    public static void PessHealth() => OnHealth?.Invoke();
    public static void PessStamina() => OnStamina?.Invoke();
    public static void PessDamage() => OnDamage?.Invoke();

   public static void PressHideBoard()=> HideBoard?.Invoke();
   public static void PressShowBoard()=> ShowBoard?.Invoke();
   public static void PressHideUI()=> HideUI?.Invoke();
   public static void PressShowUI()=> ShowUI?.Invoke();
    private void UpdateHealthBar()
    {
            // itemData1.playerData.playerHealth += itemData1.howMuchMore;
            // playerHealth.UseCoin(itemData1.price);
            // itemData1.NumberOfPurchases++;
            // itemData1.price += 50;
            // item1.coutSlideValue++;
            //StartCoroutine(item1.UpdateSliderInParts());
            item1.Buying();
            item1.CheckValue();
        SoundFXManagement.Instance.PlaySoundFXClip(clickAudio, item1.transform, .5f);
        
    }
   
    


    void HideUIFunction()
    {
        SkillsBar.transform.DOMoveY(-100, .5f).SetEase(Ease.InOutCubic);
        SliderPlayer.transform.DOMoveY(900, .5f).SetEase(Ease.InOutCubic);
      
    }  
    void ShowUIFunction()
    {
        SkillsBar.transform.DOMoveY(550, .5f).SetEase(Ease.InOutBack);
        SliderPlayer.transform.DOMoveY(500, .5f).SetEase(Ease.InOutBack);
       
    }    
    private void UpdateStaminaBar()
    {
       
            //itemData2.playerData.playerStamina += itemData2.howMuchMore;         
            //playerHealth.UseCoin(itemData2.price);
            //itemData2.NumberOfPurchases++;
            //itemData2.price += 50;
            //item2.coutSlideValue++;
            //StartCoroutine(item2.UpdateSliderInParts());
            item2.Buying();
           item2.CheckValue();
        SoundFXManagement.Instance.PlaySoundFXClip(clickAudio, item2.transform, .5f);
        // PlayerHealth.Instance.UseCoin(playerData.playerCoin)

    } 
   
        
    
    private void UpdateDamageBar()
    {
       
            item3.Buying();
        item3.CheckValue();
        SoundFXManagement.Instance.PlaySoundFXClip(clickAudio, item3.transform, .5f);
        //itemData3.playerData.playerDamage += itemData3.howMuchMore;
        ////item2.statusBar.maxValue = item2.playerData.playerStamina;
        //playerHealth.UseCoin(itemData3.price);
        //itemData3.NumberOfPurchases++;
        //itemData3.price += 50;
        //item3.coutSlideValue++;
        //StartCoroutine(item3.UpdateSliderInParts());


    }
    
    private void HidePassiveSkillsBoard()
    {
       
        passiveSkillsBoard.transform.DOMoveX(-300, .5f).SetEase(Ease.InOutCubic);
        ShowUIFunction();
        StartCoroutine(hide());
       
       // passiveSkillsBoard.SetActive(false);
    }
    IEnumerator hide()
    {
        foreach (Transform item in passiveSkillsBoard.transform)
        {
            yield return new WaitForSeconds(.1f);
            item.gameObject.SetActive(false);
        }
        passiveSkillsBoard.SetActive(false);
    }
    private void ShowPassiveSkillsBoard()
    {
        passiveSkillsBoard.SetActive(true);
         passiveSkillsBoard.transform.DOMoveX(450, .5f).SetEase(Ease.InOutBack);
        HideUIFunction();
        StartCoroutine(show());

    }
    IEnumerator show()
    {
      
        foreach (Transform item in passiveSkillsBoard.transform)
        {
            yield return new WaitForSeconds(.1f);
            item.gameObject.SetActive(true);
        }
    }
}
