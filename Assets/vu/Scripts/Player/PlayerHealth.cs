using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour,IDamageAble
{
    public static PlayerHealth Instance { get; set; }
   // public static PlayerHealth Instance;
    [SerializeField] PlayerData playerData;
    [SerializeField] float staminaRefresh;
    [SerializeField] float healthRefresh;
    [SerializeField] float timeBtweenStaminaRefresh;
    [SerializeField] float timeBtweenHealthRefresh;
    [SerializeField] float knockBackThurust;
    

    public float health
    { 
        get
        {
            return playerData.playerHealth; 
        } 
        set
        {
            playerData.playerHealth=value; 
            healthSlider.maxValue = health;
        }
    }
    public float currentHealth 
    {
        get
        { 
            return playerData.playerCurrentHealth; 
        } 
        set
        {
            playerData.playerCurrentHealth = value;
            healthSlider.value = (float)currentHealth / health;
        } 
    }
    public float stamina
    {
        get
        { 
            return playerData.playerStamina;
        } 
        set 
        { 
            playerData.playerStamina = value;
            staminaSlider.maxValue = stamina;
        }
    }
    public  float currentStamina
    { 
        get
        { 
            return playerData.playerCurrentStamina; 
        }
        set 
        { 
            playerData.playerCurrentStamina = value;
            staminaSlider.value = currentStamina / stamina;
        } 
    }

    public float currentCoin { get { return playerData.playerCoin; } set { playerData.playerCoin = value; } }

    private readonly string HEALTH_SliDER = "Health";
    private readonly string STAMINA_SliDER = "Stamina";
    private readonly string Coin_Text = "CoinText";

    Slider healthSlider;
    Slider staminaSlider;
    //Animator myAnimator;
    TextMeshProUGUI CoinText;
    Knockback Knockback;
    Rigidbody2D rb;
   public  ShopDataBase itemDamage;
    public void Die()
    {
       
    }
    public void PlayerTakeDamage(float damage,Transform damgeSource)
    {
        //TakeDamage(damage);
      //  Knockback.GetKnockBack(damgeSource, knockBackThurust);

    }
    public void TakeDamage(float damage)
    {
       
        currentHealth -=damage;
        PlayerController.Instance.myAnimator.SetTrigger(AnimationString.Hurt);
        Vector2 direction=(PlayerController.Instance.IsFacingRight)?Vector2.left:Vector2.right;
        rb.AddForce(direction*knockBackThurust, ForceMode2D.Impulse);
    }

   
    
   
    // private Transform GetTransform() { return enemyTransform; }
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
        Knockback=GetComponent<Knockback>();
        healthSlider.value = (float)currentHealth / health;
        staminaSlider.value = currentStamina / stamina;
        rb =GetComponent<Rigidbody2D>();
       
        if(Knockback == null)
        {
            Debug.Log("Dang null");
        }     
        StartCoroutine(RefreshStaminaRoutine());

       // playerData.playerMaxDamage = playerData.playeMaxLevel * itemDamage.howMuchMore;
    }
    
    // Update is called once per frame
    void Update()
    {
       
      
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
  public  IEnumerator RefreshHealthRoutine(float count, float maxHealthCanRefersh)
    {
        while (count<=maxHealthCanRefersh)
        {
            yield return new WaitForSeconds(timeBtweenHealthRefresh);
            RefreshHealth();
        }
    }
    private void RefreshHealth()
    {
        if (currentHealth <= health)
        {
            currentHealth += healthRefresh;

        }
    }
}
