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

    public PlayerData playerStatistics;
   

    private void Awake()
    {
        if (PlayerPrefs.GetInt("status") == 1)
        {
            string emailSaved = PlayerPrefs.GetString("email");
            string passwordSaved = PlayerPrefs.GetString("password");
            AutoLogin(emailSaved, passwordSaved);
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
        RegisterText.text = "Bạn đã đăng ký tài khoản thành công!";
        RegisterText.color = new Color(0, 0.636f, 0);
        currentPlayFabId = result.PlayFabId;
        Debug.Log("Bạn đã đăng ký tài khoản thành công!");
        //StartCoroutine(PleaseReturnToLogInAgain());
        //SendLeaderboard(0);
    }
    void OnErrorRegister(PlayFabError error)
    {
        Debug.Log("Đăng kí thất bại!");
        RegisterText.text = "Đăng kí thất bại!";
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
        LoginText.text = "Đăng nhập thành công!";
        LoginText.color = new Color(0, 0.636f, 0);
        Debug.Log("Đăng nhập thành công!");
        StartCoroutine(Loading());
        currentPlayFabId = result.PlayFabId;
        GetUserAccountInfo();
        LoadData();
        PlayerPrefs.SetString("email", emailInput.text);
        PlayerPrefs.SetString("password", passwordInput.text);
        PlayerPrefs.SetString("id", currentPlayFabId);
        PlayerPrefs.SetInt("status", 1);
        PlayerPrefs.Save();
    }
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(1f);
        screenMainMenuWhenLoginSuccess.SetActive(true);
        screenLogin.SetActive(false);
    }
    void OnErrorLogin(PlayFabError error)
    {
        LoginText.text = "Đăng nhập thất bại!";
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
        GetUserAccountInfo();
        PlayerPrefs.SetString("_yourCurrentPlayerID", currentPlayFabId);
        PlayerPrefs.Save();
        //Debug.Log("Bạn đã tự động đăng nhập!");
    }
    public void LogOut()
    {
        screenMainMenuWhenLoginSuccess.SetActive(false);
        LoginText.text = null;
        emailInput.text = null;
        passwordInput.text = null;
        userNameText.text = null;
        screenLogin.SetActive(true);
        SaveData();
        PlayerPrefs.SetInt("status", 0);
        PlayerPrefs.Save();
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
        userNameText.text= "Hello, "+displayName+"!";
        userNameText.color = new Color(1, 0, 0);
        PlayerPrefs.SetString("_yourName", displayName);
        string email = accountInfo.PrivateInfo.Email;
        //emailUserText.text = email;
        //IDText.text= PlayerPrefs.GetString("_yourCurrentPlayerID");
        Debug.Log($"DisplayName: {displayName}, Email: {email}");
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
            TitleId = "B44FA"
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
                {"SkillLevel1",SkillManager.Instance.IsSkillOneUnlock.ToString()},
                {"SkillLevel2",SkillManager.Instance.IsSkillTwoUnlock.ToString()},
                {"SkillLevel3",SkillManager.Instance.IsSkillThreeUnlock.ToString()}
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
            SkillManager.Instance.IsSkillOneUnlock = bool.Parse(result.Data["SkillLevel1"].Value);
            SkillManager.Instance.IsSkillTwoUnlock = bool.Parse(result.Data["SkillLevel2"].Value);
            SkillManager.Instance.IsSkillThreeUnlock = bool.Parse(result.Data["SkillLevel3"].Value);
        }
    }
    void OnDataLoadError(PlayFabError error)
    {
        Debug.Log("Không thể tải được dữ liệu của bạn!");
    }


    //Thiết lập bảng xếp hạng điểm của tất cả người chơi
    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="Smarterboard",
                    Value=score
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
        List<InfoRankPlayer> listRankPlayers= new List<InfoRankPlayer>();
        listRankPlayers.Clear();
        foreach (var item in result.Leaderboard)
        {
            listRankPlayers.Add(new InfoRankPlayer(item.PlayFabId, YourRank(item.StatValue)));
        }
        var highestScores = result.Leaderboard.Join(listRankPlayers, player1 => player1.PlayFabId, player2 => player2.playerID, (player1, player2) => new
        {
            player1.Position,
            player1.DisplayName,
            player1.StatValue,
            player1.PlayFabId,
            player2.yourRank
        });
        foreach (var item in highestScores)
        {
            if (item.PlayFabId == PlayerPrefs.GetString("_yourCurrentPlayerID"))
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = (item.Position + 1).ToString();
                texts[0].color = Color.yellow;
                texts[1].text = item.DisplayName ?? "(Blank)";
                texts[1].color = Color.yellow;
                texts[2].text = item.StatValue.ToString();
                texts[2].color = Color.yellow;
                texts[3].text = YourRank(item.StatValue);
                texts[3].color = Color.yellow;
            }
            else
            {
                GameObject newGo = Instantiate(templatePlayerPrefab, posInstance);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName ?? "(Blank)";
                texts[2].text = item.StatValue.ToString();
                texts[3].text = YourRank(item.StatValue);
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
