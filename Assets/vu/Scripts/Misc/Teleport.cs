using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string teleportNameTransitionName;
    [SerializeField] private Transform posToLoad;
    [SerializeField] private GameObject credit;

    Fade fade;
    private float waitForLoad = 1f;
    [SerializeField] bool canClear;
    bool canGo=true;
    void Start()
    {
        fade=FindAnyObjectByType<Fade>().GetComponent<Fade>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller&&controller.playerData.IsKeyUnlock&&canGo)
        {

           canGo = false;
            StartCoroutine(LoadPositonRoutine(controller.transform));
        }
    }

    private IEnumerator LoadPositonRoutine(Transform posPlayer)
    {

        fade.FadeToBlack();

        while (waitForLoad >= 0)
        {
            waitForLoad -= Time.deltaTime;
            yield return null;
        }

        if(canGo) 
        posPlayer.position=posToLoad.position;


        if(canClear) 
            fade.FadeToClear();
        else credit.gameObject.SetActive(true);


        yield return new WaitForSeconds(2f);
        canGo=true ;
    }
}
