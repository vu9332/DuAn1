using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string teleportNameTransitionName;
    [SerializeField] private Transform posToLoad;
    [SerializeField] private GameObject credit;
    [SerializeField] private PlayerData newData;
    Fade fade;
    private float waitForLoad = 1f;
    [SerializeField] bool canClear;
    bool canGo { get { return newData.canTele; } set { newData.canTele = value; } }
    void Start()
    {

        fade=FindAnyObjectByType<Fade>().GetComponent<Fade>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller&&controller.playerData.IsKeyUnlock&&canGo)
        {

           
            StartCoroutine(LoadPositonRoutine(controller.transform));
            canGo = false;
            if (KeyImage.NeedKey)
            {
                KeyImage.NeedKey = false;
            }
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
        posPlayer.position = posToLoad.position;


      

        if(canClear) 
            fade.FadeToClear();
        else credit.gameObject.SetActive(true);


        yield return new WaitForSeconds(2f);
        canGo=true ;
    }
}
