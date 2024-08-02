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

    [SerializeField] private ShopDataBase itemData1;
    [SerializeField] private item item1;

    [SerializeField] private ShopDataBase itemData2;
    [SerializeField] private item item2;

    [SerializeField] private ShopDataBase itemData3;
    [SerializeField] private item item3;

    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private GameObject passiveSkillsBoard;
    private void Update()
    {
        AutoUpdateValue();
    
    }
    void AutoUpdateValue()
    {
        item1.statusBar.value = itemData1.playerData.playerCurrentHealth;
        item2.statusBar.value = itemData1.playerData.playerCurrentStamina;
        item3.statusBar.value = itemData1.playerData.playerDamage;


        item1.statusBar.maxValue = itemData1.playerData.playerHealth;
        item2.statusBar.maxValue = itemData1.playerData.playerStamina;

    }
    private void OnEnable()
    {
        item1.statusBar.maxValue = itemData1.playerData.playerHealth;
        item2.statusBar.maxValue = itemData1.playerData.playerStamina;
        item3.statusBar.maxValue = itemData1.playerData.playerMaxDamage;


        OnStamina += UpdateStaminaBar;
        OnDamage += UpdateDamageBar;
        OnHealth += UpdateHealthBar;

        HideBoard += HidePassiveSkillsBoard;
        ShowBoard += ShowPassiveSkillsBoard;


        

    }
    private void OnDisable()
    {
      //  passiveSkillsBoard.transform.DOMove(new Vector3(-700, 0, 0), .1f);

        OnStamina -= UpdateStaminaBar;
        OnDamage -= UpdateDamageBar;
        OnHealth -= UpdateHealthBar;

        HideBoard -= HidePassiveSkillsBoard;
        ShowBoard -= ShowPassiveSkillsBoard;
    }
    public static void PessHealth() => OnHealth?.Invoke();
    public static void PessStamina() => OnStamina?.Invoke();
    public static void PessDamage() => OnDamage?.Invoke();

   public static void PressHideBoard()=> HideBoard?.Invoke();
   public static void PressShowBoard()=> ShowBoard?.Invoke();
    private void UpdateHealthBar()
    {
        if (itemData1.playerData.playerCoin >= itemData1.price)
        {
            itemData1.playerData.playerHealth += itemData1.howMuchMore;
            playerHealth.UseCoin(itemData1.price);
            itemData1.NumberOfPurchases++;
            itemData1.price += 50;
        }
    }
   


    
    private void UpdateStaminaBar()
    {
        if (itemData2.playerData.playerCoin >= itemData2.price)
        {
            itemData2.playerData.playerStamina += itemData2.howMuchMore;         
            playerHealth.UseCoin(itemData2.price);
            itemData2.NumberOfPurchases++;
            itemData2.price += 50;
        }
    } 
   
        
    
    private void UpdateDamageBar()
    {
        if (itemData3.playerData.playerCoin >= itemData3.price&& itemData1.playerData.playerMaxDamage >= itemData1.playerData.playerDamage)
        {
            itemData3.playerData.playerDamage += itemData3.howMuchMore;
            //item2.statusBar.maxValue = item2.playerData.playerStamina;
            playerHealth.UseCoin(itemData3.price);
            itemData3.NumberOfPurchases++;
            itemData3.price += 50;
        }
        else Debug.Log("del mua dc");
    }
    
    private void HidePassiveSkillsBoard()
    {
        passiveSkillsBoard.SetActive(false);
        passiveSkillsBoard.transform.DOMoveX(200, .5f).SetEase(Ease.InOutQuint);
        foreach (Transform item in  passiveSkillsBoard.transform)
        {
            item.gameObject.SetActive(false);
        }

    }
    private void ShowPassiveSkillsBoard()
    {
        passiveSkillsBoard.SetActive(true);
        foreach (Transform item in passiveSkillsBoard.transform)
        {
            item.gameObject.SetActive(true);
        }
      
    }
}
