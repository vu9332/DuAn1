using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoResultPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtUsername;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtGoldTotal;
    [SerializeField] private TextMeshProUGUI txtDeadsTotal;
    [SerializeField] private TextMeshProUGUI txtResult;
    public PlayerData playerData;
    private bool playing = false;
    void Update()
    {
        txtUsername.text = PlayerPrefs.GetString("_yourName");
        txtLevel.text = playerData.playerLevel.ToString();
        txtGoldTotal.text = playerData.playerCoin.ToString();
        //txtDeadsTotal.text= 0.ToString();
        if (playerData.playerCurrentHealth <= 0)
        {
            txtResult.text = "YOU LOSE!";
            txtResult.color = Color.red;
            if (!playing)
            {
                AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_lose_1);
                AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_lose_2);
                playing = true;
            }
        }
        else
        {
            if (!playing)
            {
                AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_win);
                playing = true;
            }
            txtResult.text = "CONGRATULATION, YOU WON!";
            txtResult.color = Color.green;
        }
    }
}
