using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMusic : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            AudioManager.Instance.PlayMusicSFX(AudioManager.Instance.logTheme);
        }
        if (SceneManager.GetActiveScene().name == "mainMenu")
        {
            AudioManager.Instance.PlayMusicSFX(AudioManager.Instance.mainMenu);
        }
    }
}
