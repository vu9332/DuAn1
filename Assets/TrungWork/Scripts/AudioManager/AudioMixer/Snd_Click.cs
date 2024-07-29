using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snd_Click : MonoBehaviour
{
    public void PlayClick()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_click_1);
    }
}
