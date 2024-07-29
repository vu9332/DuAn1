using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings Instance;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        LoadMusicVolume();
        LoadSoundVolume();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume",volume);
    }
    public void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume",0.7f);
    }

    public void SetSoundVolume()
    {
        float volume = soundSlider.value;
        PlayerPrefs.SetFloat("soundVolume", volume);
    }
    public void LoadSoundVolume()
    {
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume",0.7f);
    }
}
