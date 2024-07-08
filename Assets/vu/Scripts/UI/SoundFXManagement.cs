using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundFXManagement : MonoBehaviour
{
    public static SoundFXManagement Instance;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void PlaySoundFXClip(AudioClip clip,Transform transformSpawn,float volume)
    {
        AudioSource audi=Instantiate(soundFXObject,transformSpawn.position,Quaternion.identity);
        audi.clip = clip;

        audi.volume = volume;   

        audi.Play();

        float clipLenght=audi.clip.length;
        Destroy(audi.gameObject,clipLenght);
    }
    
}
