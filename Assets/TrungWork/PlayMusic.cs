using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    private void Start()
    {
        AudioManager.Instance.PlayMusicSFX(music);
    }
}
