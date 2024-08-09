using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingSence;
    [SerializeField] private GameObject mainMenu;


    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        loadingSence.SetActive(false);
        
    }
    public void LoadLevel(string level)
    {
        mainMenu.SetActive(false);
        loadingSence.SetActive(true);

        StartCoroutine(LoadTolevel(level));
    }

    IEnumerator LoadTolevel(string level)
    {
        AsyncOperation loadOPeration = SceneManager.LoadSceneAsync(level);

        while (!loadOPeration.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOPeration.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    } //123
}
