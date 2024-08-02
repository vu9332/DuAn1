using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    public static AudioManager Instance;

    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSFX;
    [SerializeField] AudioSource soundSFX;
    [Header("----------MusicSFX--------------")]

    public AudioClip mainMenu;
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;
    public AudioClip Level4;




    [Header("----------SoundSFX--------------")]
    [Header("Player")]

    [Header("Enemy")]
    public AudioClip snd_bringer_attack;
    public AudioClip snd_bringer_blackness;
    public AudioClip snd_bringer_death;
    public AudioClip snd_bringer_hurt;
    public AudioClip snd_demon_knives;
    public AudioClip snd_fire_splash;
    public AudioClip snd_hellball;
    public AudioClip snd_demon_death;
    public AudioClip snd_boss_Defeated;


    [Header("Other")]
    public AudioClip Hit;
    public AudioClip snd_low_pl_attack6;
    public AudioClip snd_click_1;
    public AudioClip snd_coin;
    public AudioClip snd_pick_up;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        myMixer.SetFloat("music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume",0.7f)) * 10);
        myMixer.SetFloat("sound", Mathf.Log10(PlayerPrefs.GetFloat("soundVolume", 0.7f)) * 10);
    }
    public void PlayMusicSFX(AudioClip clip)
    {
        musicSFX.clip= clip;
        musicSFX.Play();
    }
    public void StopMusicSFX(AudioClip clip)
    {
        musicSFX.clip= clip;
        musicSFX.Stop();
    }
    public void PlaySoundSFX(AudioClip clip)
    {
        soundSFX.PlayOneShot(clip);
    }
    public void PlaySoundLoopSFX(AudioClip clip)
    {
        soundSFX.clip= clip;
        soundSFX.loop= true;
        soundSFX.Play();
    }
    public void StopSoundSFX(AudioClip clip)
    {
        soundSFX.clip = clip;
        soundSFX.Stop();
    }
}
