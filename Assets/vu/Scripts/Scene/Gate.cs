using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [SerializeField] private string sceneToload;
    [SerializeField] private string sceneNameTransitionName;
  


    Fade fade;
    SceneManagement sceneManagement;
    private float waitForLoad = 1f;

    private void Awake()
    {

        fade = FindAnyObjectByType<Fade>().GetComponent<Fade>();
        sceneManagement = FindAnyObjectByType<SceneManagement>().GetComponent<SceneManagement>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller)
        {
            sceneManagement.SetTransitionName(sceneNameTransitionName);
            fade.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }

    }
    private IEnumerator LoadSceneRoutine()
    {
        while (waitForLoad >= 0)
        {
            waitForLoad -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToload);
    }
}
