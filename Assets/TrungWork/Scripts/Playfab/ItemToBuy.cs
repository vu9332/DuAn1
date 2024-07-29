using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class ItemToBuy : MonoBehaviour
{
    public TextMeshProUGUI txtcoinPrices;
    public TextMeshProUGUI itemName;
    private int coinPrices;
    private void Start()
    {
        coinPrices=int.Parse(txtcoinPrices.text);
    }

    //Mua item bằng coins
    public void BuyItems()
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CN",
            Amount = coinPrices
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinsSuccess, OnSubtractCoinsError);
    }
    void OnSubtractCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Đã mua item có tên "+ itemName.text);
        SaveAndLoadManager.instance.GetVirtualCurrency();
    }
    void OnSubtractCoinsError(PlayFabError error)
    {
        Debug.Log("Error: " + error.ErrorMessage);
    }

    //Tăng giá trị cho đơn vị tiền tệ Currency (cụ thể là Coins)
    public void GrantVirtualCurrency()
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency="CN",
            Amount = 50 
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantVirtualCurrencySuccess, OnGrantVirtualCurrencyError);
    }
    void OnGrantVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Bạn nhận được thêm 50 Coins!");
    }
    void OnGrantVirtualCurrencyError(PlayFabError error)
    {
        Debug.Log("lỗi! Bạn không thể nhận được 50 Coins :(((");
    }
}
