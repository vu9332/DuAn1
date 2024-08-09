using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class ThreadEnterTheGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textClickToPlay;
    [SerializeField] private GameObject screenLogin;
    private GameObject screenClickToPlay;
    private void Awake()
    {
        screenClickToPlay = GameObject.Find("Click to play");
    }
    void Start()
    {
        StartCoroutine(BlinkText(textClickToPlay));
        if (PlayerPrefs.GetInt("statusEnter",0) != 0)
        {
            screenLogin?.SetActive(true);
            screenClickToPlay.gameObject.SetActive(false);
        }
    }

    public void EnterTheGame()
    {
        PlayerPrefs.SetInt("statusEnter", 1);
    }
    IEnumerator BlinkText(TextMeshProUGUI textMeshProUGUI)
    {
        while (true)
        {
            textClickToPlay.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            textClickToPlay.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
