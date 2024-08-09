using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossRoom : MonoBehaviour
{
    [SerializeField]  private GameObject boss;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool _isBossWakeUp=false;
    public bool IsBossWakeUp { get { return _isBossWakeUp; } private set { _isBossWakeUp = value; } }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
            StartCoroutine(BossWakeUp());
    }
    IEnumerator BossWakeUp()
    {
        IsBossWakeUp = true;
        playerCamera.SetActive(false);
        yield return new WaitForSeconds(.7f);
        playerCamera.SetActive(true);
        GameObject ef = Instantiate(effect,spawnPoint.transform);
        yield return new WaitForSeconds(2f);
        Destroy(ef);
        GameObject b = Instantiate(boss,spawnPoint.transform);
      
        yield return new WaitForSeconds(3f);
        playerCamera.SetActive(false);
    }
    //private void SartSpawn()
    //{
    //    GameObject b = Instantiate(boss, spawnPoint.transform);
    //    playerCamera.SetActive(true);
    //}
}
