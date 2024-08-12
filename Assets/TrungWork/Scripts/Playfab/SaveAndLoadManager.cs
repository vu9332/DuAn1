using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class SaveAndLoadManager : MonoBehaviour
{
    public PlayerData playerStatistics;

    //Lưu dữ liệu của người chơi hiện tại
    public void SaveData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Level",playerStatistics.playerLevel.ToString()},
                {"Coins",playerStatistics.playerCoin.ToString()},
                {"Experiences",playerStatistics.playerExp.ToString()},
                {"SkillLevel1",playerStatistics._isSkillOneUnlock.ToString()},
                {"SkillLevel2",playerStatistics._isSkillTwoUnlock.ToString()},
                {"SkillLevel3",playerStatistics._isSkillThreeUnlock.ToString()}
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
}
