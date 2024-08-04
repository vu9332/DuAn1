using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("List of Text Aniamtion")]
    public GameObject damageTextPrefab;
    public GameObject coinTextPrefab;
    public GameObject ExpTextPrefab;

    public Canvas gameCanvas;
    public static UIManager UIManagerInstance;
    private void Start()
    {
        UIManagerInstance = this;
        gameCanvas = GameObject.Find("TrungCanvas").GetComponent<Canvas>();
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterTookItem += CharacterTookItem;
        CharacterEvents.characterTookExp += CharacterTookExp;
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterTookItem -= CharacterTookItem;
        CharacterEvents.characterTookExp -= CharacterTookExp;
    }
    public void CharacterTookDamage(GameObject character,float damageReceived)
    {
        Vector3 spamPosition=Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmp_text = Instantiate(damageTextPrefab, spamPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmp_text.text= damageReceived.ToString();
    }
    public void CharacterTookItem(GameObject obj,GameObject character, float amountReceived)
    {
        Vector3 spamPosition = Camera.main.WorldToScreenPoint(character.transform.position) + new Vector3(0,Random.Range(1f,7f),0);
        TMP_Text tmp_text = Instantiate(obj, spamPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmp_text.text = "+ "+amountReceived.ToString();
    }
    public void CharacterTookExp(GameObject obj, GameObject character, float amountReceived)
    {
        Vector3 spamPosition = Camera.main.WorldToScreenPoint(character.transform.position) + new Vector3(0, Random.Range(1f, 7f), 0);
        TMP_Text tmp_text = Instantiate(obj, spamPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmp_text.text = "+ " + amountReceived.ToString()+" EXP";
    }
}
