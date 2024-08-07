using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject panelSetting;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelLogin;
    [SerializeField] private GameObject panelReg;

    [Header("Panel Login")]
    [SerializeField] private TMP_InputField idLogin;
    [SerializeField] private TMP_InputField passLogin;
    [SerializeField] private TextMeshProUGUI loginText;

    [Header("Panel Register")]
    [SerializeField] private TMP_InputField idReg;
    [SerializeField] private TMP_InputField passReg;
    [SerializeField] private TextMeshProUGUI regText;

    [Header("Setting")]
    [SerializeField] private Slider sliderVolume;
    public AudioSource audioSource;

    [Header("Other")]
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI LoginText;
    private void Start()
    {
        panelMenu.SetActive(false);
        panelSetting.SetActive(false);
        panelLogin.SetActive(false);
        panelReg.SetActive(false);

        //audioSource = GetComponent<AudioSource>();
        //sliderVolume.value = audioSource.volume;
        //sliderVolume.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void Login()
    {
        string id = idLogin.text;
        string password = passLogin.text;
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            loginText.text = "Please enter both ID and Password.";
            loginText.color = Color.red;
        }

        if (PlayerPrefs.HasKey(id))
        {
            string storedPassword = PlayerPrefs.GetString(id);
            if (storedPassword == password)
            {
                loginText.text = "Login successful!";
                loginText.color = Color.green;

                panelLogin.SetActive(false);
                panelMenu.SetActive(true);
            }
            else
            {
                loginText.text = "Incorrect password.";
                loginText.color = Color.red;
            }
        }
        else
        {
            loginText.text = "ID not found.";
            loginText.color = Color.red;
        }
    }

    public void ActivePanel(GameObject panelTrue)
    {
        panelTrue.SetActive(true);       
    }

    public void DeActivePanel(GameObject panelFalse)
    {
        panelFalse.SetActive(false);
    }


    public void Register()
    {
        string id = idReg.text;
        string password = passReg.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            loginText.text = "Please enter both ID and Password.";
        }

        if (PlayerPrefs.HasKey(id))
        {
            regText.text = "ID already exists. Choose a different ID.";
            regText.color = Color.red;
        }
        else
        {
            PlayerPrefs.SetString(id, password);
            regText.text = "Registration successful!";
        }
    }

      

    public void NewGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        EditorApplication.isPlaying = false;

        //dung neu build game
        Application.Quit();
    }

    

}
