using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class SaveAndLoadManager : MonoBehaviour
{
    public static SaveAndLoadManager instance;
    [SerializeField] private TMP_InputField levelInput;
    [SerializeField] private TMP_InputField coinInput;
    [SerializeField] private TMP_InputField diamondInput;
    [SerializeField] private TMP_InputField energyInput;

    //hiển thị thời gian
    //[SerializeField] private TextMeshProUGUI energyRechargeTimeText;

    //float secondsLeftToRefreshEnergy = 1f;
    private void Awake()
    {
        instance = this;
    }

    //Tạo ra đồng tiền ảo mua đồ trong game

    //Load dữ liệu tiền tệ
    public void GetVirtualCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),OnGetUserInventorySuccess, OnGetUserInventoryError);
    }
    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        Debug.Log("Đã load dữ liệu thành công");
        int coins = result.VirtualCurrency["CN"];
        coinInput.text = coins.ToString();
        int diamonds = result.VirtualCurrency["DM"];
        diamondInput.text = diamonds.ToString();
        int enegy = result.VirtualCurrency["EN"];
        energyInput.text = enegy.ToString();

        //Lấy thời gian đếm ngược theo Recharge Time (unit per day)
        //secondsLeftToRefreshEnergy = result.VirtualCurrencyRechargeTimes["EN"].SecondsToRecharge;
    }

    //Chạy thời gian cooldown hồi thể lực
    //private void Update()
    //{
    //    secondsLeftToRefreshEnergy -= Time.deltaTime;
    //    TimeSpan time=TimeSpan.FromSeconds(secondsLeftToRefreshEnergy);
    //    energyRechargeTimeText.text = time.ToString("mm':'ss");
    //    if(secondsLeftToRefreshEnergy < 0)
    //    {
    //        GetVirtualCurrency();
    //    }
    //}
    void OnGetUserInventoryError(PlayFabError error)
    {
        Debug.Log("Lỗi không thể load dữ liệu!");
    }

    //Lưu dữ liệu của người chơi hiện tại
    public void SaveData()
    {
        var request = new UpdateUserDataRequest
        {
            Data=new Dictionary<string, string>
            {
                {"Level",levelInput.text},
                {"Coins",coinInput.text},
                {"Diamonds",diamondInput.text}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSaveDataSuccess, OnSaveDataError);
    }
    void OnSaveDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Đã lưu lại dữ liệu lên Playfab");
    }
    void OnSaveDataError(PlayFabError error)
    {
        Debug.Log("Lỗi không thể lưu lại dữ liệu!");
    }

    //Load dữ liệu của người chơi hiện tại
    public void LoadData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnDataLoadError);
    }
    void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("Dữ liệu của bạn đã được load!");
        if(result.Data != null && result.Data.ContainsKey("Level") && result.Data.ContainsKey("Coins") && result.Data.ContainsKey("Diamonds"))
        {
            levelInput.text = result.Data["Level"].Value;
            coinInput.text = result.Data["Coins"].Value;
            diamondInput.text = result.Data["Diamonds"].Value;
        }
    }
    void OnDataLoadError(PlayFabError error)
    {
        Debug.Log("Không thể tải được dữ liệu của bạn!");
    }
}
