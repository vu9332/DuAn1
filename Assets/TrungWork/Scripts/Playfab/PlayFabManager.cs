using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab.EconomyModels;
using Unity.VisualScripting;
using TMPro;
using UnityEditor.PackageManager.Requests;
using PlayFab.MultiplayerModels;
using System.Linq;
using System.Globalization;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;

public class InfoRankPlayer
{
    public string playerID { get; set; }
    public string yourRank {  get; set; }
    public InfoRankPlayer(string _playerID, string _rank)
    {
        playerID = _playerID;
        yourRank = _rank;
    }
}
public class PlayFabManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RegisterText, LoginText;
    public TMP_InputField emailInput, passwordInput,userNameInput;
    public GameObject screenSignUp, screenLogin,screenMainMenuWhenLoginSuccess,screenResetPassword;
    public GameObject templatePlayerPrefab;
    public Transform posInstance;
    public TextMeshProUGUI scoreHighText, userNameText, emailUserText, IDText;
    private string currentPlayFabId;
    public TextMeshProUGUI titleUsernameText;
    public PlayerData playerStatistics;

    public GameObject leaderBoardObj;
    public Button btnPlay;
    public Button btnLeaderboard;
    public Button btnlogOut;
    public Button btnExit;
    public Button btnSetting;
    private void Awake()
    {
        //if (PlayerPrefs.GetInt("status") == 1)
        //{
        //    string emailSaved = PlayerPrefs.GetString("email");
        //    string passwordSaved = PlayerPrefs.GetString("password");
        //    AutoLogin(emailSaved, passwordSaved);
        //}
    }
    private void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name== "mainMenu")
        {
            GetUserAccountInfo();
        }
    }
    //Hiển thị màn hình đăng nhập và đăng kí tài khoản

    public void goToSignUp()
    {
        emailInput.text = null;
        passwordInput.text = null;
        LoginText.text = null;
        RegisterText.text = null;
    }
    public void backtoLogin()
    {
        emailInput.text = null;
        passwordInput.text = null;
        userNameInput.text = null;
        LoginText.text = null;
        RegisterText.text = null;
    }
    public void returnToLogin()
    {
        screenLogin.SetActive(true);
        screenSignUp.SetActive(false);
        emailInput.text = null;
        passwordInput.text = null;
        userNameInput.text = null;
        LoginText.text = null;
        RegisterText.text = null;
    }
    public void IWantResetPassword()
    {
        emailInput.text = null;
        passwordInput.text = null;
        LoginText.text = null;
        screenLogin.SetActive(false);
        screenResetPassword.SetActive(true);
    }
    public void IDontWantResetPassword()
    {
        emailInput.text = null;
        screenLogin.SetActive(true);
        screenResetPassword.SetActive(false);
        LoginText.text=null;
    }
    public void HideText()
    {
        titleUsernameText.gameObject.SetActive(false);
        userNameText.text = null;
    }
    public void DisplayText()
    {
        titleUsernameText.gameObject.SetActive(true);
        titleUsernameText.text = "HELLO,";
        userNameText.text = PlayerPrefs.GetString("_yourName")+"!";
    }
    public void DisplayLeaderBoardPlease()
    {
        leaderBoardObj.SetActive(true);
        btnPlay.GetComponent<Button>().enabled = false;
        btnLeaderboard.GetComponent<Button>().enabled = false;
        btnSetting.GetComponent<Button>().enabled = false;
        btnlogOut.GetComponent<Button>().enabled = false;
        btnExit.GetComponent<Button>().enabled = false;
    }
    public void HideLeaderBoardPlease()
    {
        leaderBoardObj.SetActive(false);
        btnPlay.GetComponent<Button>().enabled = true;
        btnLeaderboard.GetComponent<Button>().enabled = true;
        btnSetting.GetComponent<Button>().enabled = true;
        btnlogOut.GetComponent<Button>().enabled = true;
        btnExit.GetComponent<Button>().enabled = true;
    }
    //Thiết lập nút đăng kí tài khoản và lưu lên PlayFab
    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            DisplayName = userNameInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnErrorRegister);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        RegisterText.text = "You have successfully registered a new account!";
        RegisterText.color = new Color(0, 0.636f, 0);
        currentPlayFabId = result.PlayFabId;
        Debug.Log("Bạn đã đăng ký tài khoản thành công!");
        //StartCoroutine(PleaseReturnToLogInAgain());
    }
    void OnErrorRegister(PlayFabError error)
    {
        Debug.Log("Đăng kí thất bại!");
        RegisterText.text = "Account registration failed!";
        RegisterText.color = Color.red;
    }
    IEnumerator PleaseReturnToLogInAgain()
    {
        yield return new WaitForSeconds(1);
        returnToLogin();
    }
    //Thiết lập nút đăng nhập vào game
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnErrorLogin);
    }
    void OnLoginSuccess(LoginResult result)
    {
        LoginText.text = "Login successfully!";
        LoginText.color = new Color(0, 0.636f, 0);
        Debug.Log("Đăng nhập thành công!");
        StartCoroutine(Loading());
        currentPlayFabId = result.PlayFabId;
        LoadData();
        PlayerPrefs.SetString("email", emailInput.text);
        PlayerPrefs.SetString("password", passwordInput.text);
        PlayerPrefs.SetString("id", currentPlayFabId);
        PlayerPrefs.SetInt("status", 1);
        PlayerPrefs.Save();
    }
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(0.5f);
        //AsyncOperation t = SceneManager.LoadSceneAsync("mainMenu");
        //while (!t.isDone)
        //{
        //    yield return null;
        //}
        SceneManager.LoadScene("mainMenu");
        yield return new WaitForSeconds(0.5f);
        GetUserAccountInfo();
        GetLeaderboard();
        DisplayText();
    }
    IEnumerator UpdateTextOnLoginScreen()
    {
        AsyncOperation t = SceneManager.LoadSceneAsync("Menu2 1");
        while(!t.isDone)
        {
            yield return null;
        }
        if(t.isDone)
        {
            emailInput.text = null;
            passwordInput.text = null;
            LoginText.text = null;
        }
    }
    void OnErrorLogin(PlayFabError error)
    {
        LoginText.text = "Login failed!";
        LoginText.color = Color.red;
        Debug.Log("Đăng nhập thất bại!");
    }


    //Thiết lập chức năng tự động đăng nhập vào trong game
    void AutoLogin(string _email,string _password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = _email,
            Password = _password,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnAutoLoginSuccess,OnError);
    }
    void OnAutoLoginSuccess(LoginResult result)
    {
        currentPlayFabId = PlayerPrefs.GetString("id");
        PlayerPrefs.SetString("_yourCurrentPlayerID", currentPlayFabId);
        PlayerPrefs.Save();
        //Debug.Log("Bạn đã tự động đăng nhập!");
    }
    void HideAllInputText()
    {
        LoginText.text = null;
        emailInput.text = null;
        passwordInput.text = null;
        userNameText.text = null;
    }
    public void LogOut()
    {
        StartCoroutine(UpdateTextOnLoginScreen());
        SaveData();
        PlayerPrefs.SetInt("status", 0);
        PlayerPrefs.Save();
        SendLeaderboard(playerStatistics.playerLevel);
    }

    // Lấy thông tin tài khoản người dùng sau khi đăng nhập
    public void GetUserAccountInfo()
    {
        var request = new GetAccountInfoRequest
        {
            PlayFabId = currentPlayFabId
        };
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnError);
    }

    void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        var accountInfo = result.AccountInfo;
        string displayName = accountInfo.TitleInfo.DisplayName;
        userNameText.text=displayName+"!";
        userNameText.color = new Color(0.009217262f, 1, 0);
        titleUsernameText.text = "HELLO,";
        titleUsernameText.color=Color.black;
        PlayerPrefs.SetString("_yourName", displayName);
        string email = accountInfo.PrivateInfo.Email;
        //emailUserText.text = email;
        //IDText.text= PlayerPrefs.GetString("_yourCurrentPlayerID");
    }

    // Lấy thống kê người chơi
    public void GetUserStatistics()
    {
        var request = new GetPlayerStatisticsRequest();
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetPlayerStatisticsSuccess, OnError);
    }

    void OnGetPlayerStatisticsSuccess(GetPlayerStatisticsResult result)
    {
        var highestStat = result.Statistics
            .Where(stat => stat.StatisticName == "Smarterboard")
            .OrderByDescending(stat => stat.Value)
            .FirstOrDefault();

        if (highestStat != null)
        {
            //Debug.Log($"Highest StatValue: {highestStat.Value}");
            scoreHighText.text=highestStat.Value.ToString();
            PlayerPrefs.SetInt("highScore", highestStat.Value);
            PlayerPrefs.Save();
        }
    }
    //Thiết lập nút khôi phục lại mật khẩu cho tài khoản đã có trước đó
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "5EE4D"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnErrorResetPassword);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Email yêu cầu khôi phục đã được gửi vui lòng kiểm tra!");
        LoginText.text = "Email yêu cầu khôi phục đã được gửi vui lòng kiểm tra!";
        LoginText.color = new Color(0, 0.636f, 0);
    }
    

    //Thông báo lỗi phát sinh
    void OnError(PlayFabError error)
    {
        //Debug.Log("Đăng nhập thất bại");
    }
    void OnErrorResetPassword(PlayFabError error)
    {
        LoginText.text = "Khôi phục thất bại!";
        LoginText.color = Color.red;
    }

    //Lưu dữ liệu của người chơi hiện tại
    public void SaveData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Level",playerStatistics.playerLevel.ToString()},
                {"Coins",playerStatistics.playerCoin.ToString()},
                {"Experiences",playerStatistics.playerExp.ToString()},
                {"SkillLevel1",playerStatistics._isSkillOneUnlock.ToString()},
                {"SkillLevel2",playerStatistics._isSkillTwoUnlock.ToString()},
                {"SkillLevel3",playerStatistics._isSkillThreeUnlock.ToString()}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSaveDataSuccess, OnSaveDataError);
    }
    void OnSaveDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Đã lưu lại dữ liệu lên Playfab");
    }
    void OnSaveDataError(PlayFabError error)
    {
        Debug.Log("Lỗi không thể lưu lại dữ liệu!");
    }

    //Load dữ liệu của người chơi hiện tại
    public void LoadData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnDataLoadError);
    }
    void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("Dữ liệu của bạn đã được load!");
        if (result.Data != null 
        && result.Data.ContainsKey("Level") && result.Data.ContainsKey("Coins") && result.Data.ContainsKey("Experiences") && result.Data.ContainsKey("SkillLevel1")
        && result.Data.ContainsKey("SkillLevel2") && result.Data.ContainsKey("SkillLevel3"))
        {
            playerStatistics.playerLevel = int.Parse(result.Data["Level"].Value);
            playerStatistics.playerCoin = float.Parse(result.Data["Coins"].Value);
            playerStatistics.playerExp = float.Parse(result.Data["Experiences"].Value);
            playerStatistics._isSkillOneUnlock = bool.Parse(result.Data["SkillLevel1"].Value);
            playerStatistics._isSkillTwoUnlock = bool.Parse(result.Data["SkillLevel2"].Value);
            playerStatistics._isSkillThreeUnlock = bool.Parse(result.Data["SkillLevel3"].Value);
        }
        else
        {
            playerStatistics.playerLevel = 1;
            SendLeaderboard(playerStatistics.playerLevel);
            playerStatistics.playerCoin = 0;
            playerStatistics.playerExp = 0;
            playerStatistics._isSkillOneUnlock = true;
            playerStatistics._isSkillTwoUnlock = false;
            playerStatistics._isSkillThreeUnlock = false;
            SaveData();
        }
    }
    void OnDataLoadError(PlayFabError error)
    {
        Debug.Log("Không thể tải được dữ liệu của bạn!");
    }


    //Thiết lập bảng xếp hạng điểm của tất cả người chơi
    public void SendLeaderboard(int level)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="Smarterboard",
                    Value=level
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Bảng xếp hạng đã được thiết lập!");
    }
    //Hiển thị bảng xếp hạng theo điểm giảm dần
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Smarterboard",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, DisplayLeaderboardGet, OnError);
    }
    void DisplayLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in posInstance)
        {
            Destroy(item.gameObject);
        }
        var highestScores = result.Leaderboard.Take(10);
        foreach (var item in highestScores)
        {
            if (item.PlayFabId == PlayerPrefs.GetString("id"))
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = (item.Position + 1).ToString();
                texts[0].color = new Color(0, 0.6682706f, 1);
                texts[1].text = item.DisplayName ?? "(Blank)";
                texts[1].color = new Color(0, 0.6682706f, 1);
                texts[2].text = item.StatValue.ToString();
                texts[2].color = new Color(0, 0.6682706f, 1);
            }
            else
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName ?? "(Blank)";
                texts[2].text = item.StatValue.ToString();
            }
        }
    }

    //Xuất danh sách người chơi có điểm bé hơn điểm của người chơi hiện tại
    public void GetLowerScoreThanPlayerLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Smarterboard",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLowerScoreThanPlayerLeaderboard, OnError);
    }
    void OnGetLowerScoreThanPlayerLeaderboard(GetLeaderboardResult result)
    {
        foreach (Transform item in posInstance)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            if (item.PlayFabId == PlayerPrefs.GetString("_yourCurrentPlayerID"))
            {
                var result1 = result.Leaderboard.Where(a => a.StatValue < item.StatValue).ToList();
                foreach (var item1 in result1)
                {
                    GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                    TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                    texts[0].text = (item1.Position + 1).ToString();
                    texts[1].text = item1.DisplayName ?? "(Blank)";
                    texts[2].text = item1.StatValue.ToString();
                    texts[3].text = YourRank(item1.StatValue);
                }
            }
        }
    }

    //Xuất danh sách 5 người chơi có điểm thấp nhất theo thứ tự tăng dần
    public void Get5LowestScoreLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Smarterboard",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGet5LowestScoreLeaderboard, OnError);
    }
    void OnGet5LowestScoreLeaderboard(GetLeaderboardResult result)
    {
        foreach (Transform item in posInstance)
        {
            Destroy(item.gameObject);
        }
        var result2=result.Leaderboard.OrderBy(item => item.StatValue).ThenBy(item => item.DisplayName).Take(5);
        int pos = 1;
        foreach (var item2 in result2)
        {
            if (item2.PlayFabId == PlayerPrefs.GetString("_yourCurrentPlayerID"))
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = pos.ToString();
                texts[1].text = item2.DisplayName ?? "(Blank)";
                texts[2].text = item2.StatValue.ToString();
                texts[3].text = YourRank(item2.StatValue);
                texts[0].color= Color.yellow;
                texts[1].color= Color.yellow;
                texts[2].color= Color.yellow;
                texts[3].color= Color.yellow;
                pos++;
            }
            else
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = pos.ToString();
                texts[1].text = item2.DisplayName ?? "(Blank)";
                texts[2].text = item2.StatValue.ToString();
                texts[3].text = YourRank(item2.StatValue);
                pos++;
            }
        }
    }
    //Xuất danh sách 5 người chơi có điểm cao nhất theo thứ tự tăng dần
    public void Get5BestScoreLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Smarterboard",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGet5BestScoreLeaderboard, OnError);
    }
    void OnGet5BestScoreLeaderboard(GetLeaderboardResult result)
    {
        foreach (Transform item in posInstance)
        {
            Destroy(item.gameObject);
        }
        var result2 = result.Leaderboard.OrderByDescending(item => item.StatValue).ThenBy(item => item.DisplayName).Take(5);
        int pos = 1;
        foreach (var item2 in result2)
        {
            if (item2.PlayFabId == PlayerPrefs.GetString("_yourCurrentPlayerID"))
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = pos.ToString();
                texts[1].text = item2.DisplayName ?? "(Blank)";
                texts[2].text = item2.StatValue.ToString();
                texts[3].text = YourRank(item2.StatValue);
                texts[0].color = Color.yellow;
                texts[1].color = Color.yellow;
                texts[2].color = Color.yellow;
                texts[3].color = Color.yellow;
                pos++;
            }
            else
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = pos.ToString();
                texts[1].text = item2.DisplayName ?? "(Blank)";
                texts[2].text = item2.StatValue.ToString();
                texts[3].text = YourRank(item2.StatValue);
                pos++;
            }
        }
    }
    public string YourRank(int _yourScore)
    {
        return _yourScore switch
        {
            0 => "Chưa xếp hạng",
            < 5 => "Đồng",
            < 8 => "Bạc",
            < 10 => "Vàng",
            _ => "Kim cương"
        };
    }
}
