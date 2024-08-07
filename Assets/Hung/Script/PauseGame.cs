using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using static StatusManagement;


public class PauseGame : MonoBehaviour
{
    
    [Header("Pause Panel")]
    [SerializeField] private GameObject pausePanel;

    [Header("Setting Pause")]
    [SerializeField] private GameObject pauseSetting;
    bool isPause=false;

    private void Start()
    {
        pausePanel.SetActive(false);
        pauseSetting.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isPause)

            {
                isPause = true;
                pausePanel.SetActive(isPause);
               
            }
            else if (isPause)
            {
                isPause = false;
                pausePanel.SetActive(isPause);
            }
            // = !pausePanel.activeSelf;



            if (isPause)
            {
                StatusManagement.PressHideBoard();
                StartCoroutine(delay());
                
            }
            else
            {
                Time.timeScale = 1f;
                
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

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;

    }




}
