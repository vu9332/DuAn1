using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusicSFX(AudioManager.Instance.mainMenu);
    }
}
