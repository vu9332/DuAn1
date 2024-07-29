using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public GameObject panelOptions;
    public void pauseGame()
    {
        Time.timeScale = 0;
        panelOptions.SetActive(true);
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
        panelOptions.SetActive(false);
    }
}
