using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    public static string sceneTransitionName { get; private set; }

    public void SetTransitionName(string transitionName)
    {
        sceneTransitionName = transitionName;
    }
}
