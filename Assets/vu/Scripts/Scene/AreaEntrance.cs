using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    [SerializeField] private Transform playerTransform;
    SceneManagement sceneManagement;

    Fade fade;

    private void Start()
    {
        //  so=FindAnyObjectByType<SO>().GetComponent<SO>();    
        fade = FindAnyObjectByType<Fade>().GetComponent<Fade>();

        if (transitionName == SceneManagement.sceneTransitionName)
        {
            playerTransform.transform.position = this.transform.position;
            fade.FadeToClear();
            Debug.Log("Do");

        }
        else
        {
            Debug.Log("Do else");
        }

    }
}
