using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public static SliderManager Instance;
    private readonly string HEALTH_SliDER = "Health";
    private readonly string STAMINA_SliDER = "Stamina";
    private readonly string Coin_Text = "CoinText";
    public Slider healthSlider { get; set; }
    public  Slider staminaSlider { get; set; }
    TextMeshProUGUI CoinText;

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI currentStaminaText;
    private void Start()
    {
        healthSlider = GameObject.Find(HEALTH_SliDER).GetComponent<Slider>();
        staminaSlider = GameObject.Find(STAMINA_SliDER).GetComponent<Slider>();
        CoinText = GameObject.Find(Coin_Text).GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        UpdateValue();
    }
    void  UpdateValue()
    {
        healthSlider.value = (float)PlayerHealth.Instance.currentHealth / PlayerHealth.Instance.health;
        staminaSlider.value = PlayerHealth.Instance.currentStamina / PlayerHealth.Instance.stamina;
        CoinText.text =PlayerHealth.Instance.currentCoin.ToString();
        currentHealthText.text=PlayerHealth.Instance.currentHealth+"/"+PlayerHealth.Instance.health;
        currentStaminaText.text=PlayerHealth.Instance.currentStamina+"/"+PlayerHealth.Instance.stamina;
    }    
}
