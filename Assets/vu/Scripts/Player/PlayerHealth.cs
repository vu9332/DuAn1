using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IDamageAble
{
   // public static PlayerHealth Instance;
    [SerializeField] PlayerData playerData;
    [SerializeField] float staminaRefresh;
    [SerializeField] float timeBtweenStaminaRefresh;

    public float health { get { return playerData.playerHealth; } set { playerData.playerHealth= value; } }
    public float currentHealth;
    public float stamina { get { return playerData.playerStamina; } set { playerData.playerStamina = value; } }
  public  float currentStamina;

    private readonly string HEALTH_SliDER = "Health";
    private readonly string STAMINA_SliDER = "Stamina";

    Slider healthSlider;
    Slider staminaSlider;
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

    // Start is called before the first frame update
    void Start()
    {
        healthSlider =GameObject.Find(HEALTH_SliDER).GetComponent<Slider>();
        staminaSlider =GameObject.Find(STAMINA_SliDER).GetComponent<Slider>();
        currentHealth =health;
        currentStamina= stamina;
        healthSlider.maxValue = health;
        staminaSlider.maxValue= stamina; 
        StartCoroutine(RefreshStaminaRoutine());    
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value=currentHealth;
        staminaSlider .value=currentStamina;    
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
