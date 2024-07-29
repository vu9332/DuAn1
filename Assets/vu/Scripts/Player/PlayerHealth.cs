using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour,IDamageAble
{
    public static PlayerHealth Instance { get; set; }
   // public static PlayerHealth Instance;
    [SerializeField] PlayerData playerData;
    [SerializeField] float staminaRefresh;
    [SerializeField] float timeBtweenStaminaRefresh;

    public float health { get { return playerData.playerHealth; } set { playerData.playerHealth= value; healthSlider.maxValue = health; } }
    public float currentHealth;
    public float stamina { get { return playerData.playerStamina; } set { playerData.playerStamina = value; staminaSlider.maxValue = stamina; } }
    public  float currentStamina;

    public float currentCoin { get { return playerData.playerCoin; } set { playerData.playerCoin = value; } }

    private readonly string HEALTH_SliDER = "Health";
    private readonly string STAMINA_SliDER = "Stamina";
    private readonly string Coin_Text = "CoinText";

    Slider healthSlider;
    Slider staminaSlider;
    
    TextMeshProUGUI CoinText;
    public void Die()
    {
       
    }

    public void TakeDamage(float damage)
    {
        currentHealth-=damage;

    }
    public void UseStamina(float staminaUse)
    {
        currentStamina-=staminaUse;
    }
  public  void UseCoin(float coinUse)
    {
        currentCoin-=coinUse;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider =GameObject.Find(HEALTH_SliDER).GetComponent<Slider>();
        staminaSlider =GameObject.Find(STAMINA_SliDER).GetComponent<Slider>();
        CoinText = GameObject.Find(Coin_Text).GetComponent<TextMeshProUGUI>();
        currentHealth =health;
        currentStamina= stamina;
        
       
        StartCoroutine(RefreshStaminaRoutine());    
    }
    
    // Update is called once per frame
    void Update()
    {
        healthSlider.value=currentHealth;
        staminaSlider .value=currentStamina;
        CoinText.text = currentCoin.ToString();
    }
    private void RefreshStamina()
    {
        if (currentStamina < stamina)
        {
            currentStamina += staminaRefresh;

        }
    }
    IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBtweenStaminaRefresh);
            RefreshStamina();
        }
    }
}
