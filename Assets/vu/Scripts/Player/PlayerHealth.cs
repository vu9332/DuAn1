using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IDamageAble
{
   // public static PlayerHealth Instance;
    [SerializeField] float maxHealth;
    [SerializeField] float maxStamina;
    [SerializeField] float staminaRefresh;
    [SerializeField] float timeBtweenStaminaRefresh;

    public float health { get { return maxHealth; } set { maxHealth = value; } }
    public float currentHealth { get; set; }
    // public float stamina { get { return maxStamina; } set { maxStamina = value; } }
    public float currentStamina { get; set; }

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
        currentStamina=maxStamina;
        healthSlider.maxValue = maxHealth;
        staminaSlider.maxValue= maxStamina; 
        StartCoroutine(RefreshStaminaRoutine());    
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value=currentHealth;
        staminaSlider .value=currentStamina;    
    }
    public void RefreshStamina()
    {
        if (currentStamina<maxStamina)
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
