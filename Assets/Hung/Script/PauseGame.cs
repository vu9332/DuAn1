using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class PauseGame : MonoBehaviour
{
    
    [Header("Pause Panel")]
    [SerializeField] private GameObject pausePanel;

    [Header("Setting Pause")]
    [SerializeField] private GameObject pauseSetting;

    private void Start()
    {
        pausePanel.SetActive(false);
        pauseSetting.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            bool isPause = !pausePanel.activeSelf;
            pausePanel.SetActive(isPause);

            if(isPause )
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time .timeScale = 1f;
            }
        }
        
    }
    public void DeActivePanel(GameObject panelActive)
    {
        panelActive.SetActive(false);
    }

    public void ActivePanel(GameObject panelDeActive)
    {
        panelDeActive.SetActive(true);
    }

    public void Resume()
    {        
        Time.timeScale = 1.0f;
    }

    public void BackToMenu()
    {
        //them phan luu data cua nv vao
        SceneManager.LoadScene("Menu2");
    }

    public void Exit()
    {
        EditorApplication.isPlaying = false;

        Application.Quit();
    }


    



}
