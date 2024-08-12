using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossRoom : MonoBehaviour
{
    public static OpenBossRoom Instance {  get; private set; }  

    [SerializeField]  private GameObject boss;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject miniMap;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool _isBossWakeUp=false;
    public bool IsBossWakeUp { get { return _isBossWakeUp; } private set { _isBossWakeUp = value; } }

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private float timeShake;
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        boss.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pl=collision.gameObject.GetComponent<PlayerController>();
        if(pl!=null&&pl.playerData.IsKeyUnlock)
        {
          //  pl.playerData.IsKeyUnlock = false;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null&&!IsBossWakeUp)
        {
            StartCoroutine(BossWakeUp());
          
        }
         
    }
    IEnumerator BossWakeUp()
    {
        SoundFXManagement.Instance.PlaySoundFXClip(audioClips[0], transform, 1f);
        IsBossWakeUp = true;
        StartCoroutine(ShakeCam());
        yield return new WaitForSeconds(.7f);
       // playerCamera.SetActive(true);
        GameObject ef = Instantiate(effect,spawnPoint.transform);
        yield return new WaitForSeconds(1.5f);
        //GameObject b = Instantiate(boss,spawnPoint.transform);
        boss.gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);
        ef.SetActive(false);
      //  playerCamera.SetActive(false);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    IEnumerator ShakeCam()
    {
        float elapsed = 0;
        while (elapsed <= timeShake)
        {
            elapsed += Time.deltaTime;
            yield return null;
            CameraShake.instance.ShakeCamera(5);
        }
    }
    //private void SartSpawn()
    //{
    //    GameObject b = Instantiate(boss, spawnPoint.transform);
    //    playerCamera.SetActive(true);
    //}
}
