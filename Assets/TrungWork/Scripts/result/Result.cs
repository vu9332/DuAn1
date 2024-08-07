using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtUsername;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtGoldTotal;
    [SerializeField] private TextMeshProUGUI txtDeadsTotal;
    [SerializeField] private TextMeshProUGUI txtResult;
    public PlayerData playerData;
    private void Start()
    {
        txtUsername.text = PlayerPrefs.GetString("_yourName");
        txtLevel.text= playerData.playerLevel.ToString();
        txtGoldTotal.text=playerData.playerCoin.ToString();
        
    }
    private void Update()
    {
        // Nếu người chơi chơi hết 3 level và đánh thắng boss cuối thì hiện text You win!
        //txtResult.text = "You win!";


        //// Nếu người chơi chơi hết mạng trước khi thắng boss cuối hiện text You lose!
     
    }
}
