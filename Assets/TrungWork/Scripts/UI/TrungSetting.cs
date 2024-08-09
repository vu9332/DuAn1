using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrungSetting : MonoBehaviour
{
    public GameObject panelSetting;
    public Button btnSetting;
    public Button closeSetting;
    public Button btnclickToPlay;

    public delegate void OnButtonClick();
    public event OnButtonClick OnSettingVolume;
    public event OnButtonClick OffSettingVolume;

    public void OnEnable()
    {
        OnSettingVolume += DisplaySetting;
        OffSettingVolume += HideSetting;
    }
    public void DisplayVolumeSetting()
    {
        OnSettingVolume();
    }
    public void HideVolumeSetting()
    {
        OffSettingVolume();
    }
    private void Update()
    {
        btnSetting.onClick.AddListener(DisplayVolumeSetting);
        closeSetting.onClick.AddListener(HideVolumeSetting);
    }
    public void HideSetting()
    {
        panelSetting.SetActive(false);
    }
    public void DisplaySetting()
    {
        panelSetting.SetActive(true);
    }
    public void NewGame()
    {
        //SceneManager.LoadScene(2);
        AsyncOperation t = SceneManager.LoadSceneAsync(2);
    }
}
