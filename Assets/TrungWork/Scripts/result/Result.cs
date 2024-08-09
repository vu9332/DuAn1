using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private GameObject panelResult;
    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        panelResult.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        AsyncOperation t = SceneManager.LoadSceneAsync("mainMenu");
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(2);
        panelResult.SetActive(false);
        playerData.playerCurrentHealth=playerData.playerHealth;
        playerData.playerCurrentStamina=playerData.playerStamina;
    }
 
}
