using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoLogin : MonoBehaviour
{
    static AutoLogin()
    {
        EditorApplication.playModeStateChanged += AutoLoginWhenYouOpenTheGame;
    }
    private static void AutoLoginWhenYouOpenTheGame(PlayModeStateChange state)
    {
        //switch (state)
        //{
        //    case PlayModeStateChange.EnteredPlayMode:
        //        if (PlayerPrefs.GetInt("status") == 1)
        //        {
        //            //SceneManager.LoadScene(1);
        //            PlayFabManager screenMainMenu = FindAnyObjectByType<PlayFabManager>().GetComponent<PlayFabManager>();
        //            screenMainMenu.screenLogin.SetActive(false);
        //            screenMainMenu.screenMainMenuWhenLoginSuccess.SetActive(true);
        //            Debug.Log("Bạn đã tự động đăng nhập!");
        //            TextMeshProUGUI d = FindAnyObjectByType<Menu>().GetComponent<Menu>().userNameText;
        //            d.text = PlayerPrefs.GetString("currentPlayerName");
        //        }
        //        // Thực hiện hành động khi vào chế độ Play Mode
        //        break;

        //    case PlayModeStateChange.ExitingPlayMode:
        //        PlayFabManager screenMainMenu1 = FindAnyObjectByType<PlayFabManager>().GetComponent<PlayFabManager>();
        //        screenMainMenu1.screenLogin.SetActive(true);
        //        screenMainMenu1.screenMainMenuWhenLoginSuccess.SetActive(false);
        //        TextMeshProUGUI b = FindAnyObjectByType<Menu>().GetComponent<Menu>().userNameText;
        //        PlayerPrefs.SetString("currentPlayerName", b.text);
        //        TextMeshProUGUI f = FindAnyObjectByType<Menu>().GetComponent<Menu>().LoginText;
        //        f.text = null;
        //        screenMainMenu1.emailInput.text = null;
        //        screenMainMenu1.passwordInput.text = null;
        //        // Thực hiện hành động khi thoát khỏi chế độ Play Mode
        //        break;

        //    case PlayModeStateChange.EnteredEditMode:
        //        if (PlayerPrefs.GetInt("status") == 1)
        //        {
        //            PlayFabManager screenMainMenu2 = FindAnyObjectByType<PlayFabManager>().GetComponent<PlayFabManager>();
        //            screenMainMenu2.screenLogin.SetActive(false);
        //            screenMainMenu2.screenMainMenuWhenLoginSuccess.SetActive(true);
        //            TextMeshProUGUI a = FindAnyObjectByType<Menu>().GetComponent<Menu>().userNameText;
        //            a.text = null;
        //        }
        //        else
        //        {
        //            PlayFabManager screenMainMenu2 = FindAnyObjectByType<PlayFabManager>().GetComponent<PlayFabManager>();
        //            screenMainMenu2.screenLogin.SetActive(true);
        //            screenMainMenu2.screenMainMenuWhenLoginSuccess.SetActive(false);
        //        }
        //        // Thực hiện hành động khi trở về chế độ Edit Mode
        //        break;
        //}

    }
}
