using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public Canvas gameCanvas;
    public static UIManager UIManagerInstance;
    private void Start()
    {
        UIManagerInstance = this;
        gameCanvas=FindAnyObjectByType<Canvas>();
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
    }
    public void CharacterTookDamage(GameObject character,float damageReceived)
    {
        Vector3 spamPosition=Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmp_text = Instantiate(damageTextPrefab, spamPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmp_text.text= damageReceived.ToString();
    }
}
