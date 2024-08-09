using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour,IDamageAble
{
    public static PlayerHealth Instance { get; set; }
   // public static PlayerHealth Instance;
    [SerializeField] PlayerData playerData;
    [SerializeField] float staminaRefresh;
    [SerializeField] float healthRefresh;
    [SerializeField] float timeBtweenStaminaRefresh;
    [SerializeField] float timeBtweenHealthRefresh;
    [SerializeField] float cameraShakeForce;


    [SerializeField] private bool _isDeath=false;
    public bool IsDeath { get { return _isDeath;  } set { _isDeath = value; } }
    public float health
    { 
        get
        {
            return playerData.playerHealth; 
        } 
        set
        {
            playerData.playerHealth=value; 
          SliderManager.Instance.healthSlider.maxValue = health;
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
            //SliderManager.Instance.healthSlider.value = (float)currentHealth / health;
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
            SliderManager.Instance.staminaSlider.maxValue = stamina;
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
            //staminaSlider.value = currentStamina / stamina;
        } 
    }

    public float currentCoin { get { return playerData.playerCoin; } set { playerData.playerCoin = value; } }


    // Animator myAnimator;
 
    Knockback Knockback;
    Flash flash;
    Rigidbody2D rb;
    [SerializeField] private GameObject playerDie;
   // [SerializeField] private GameObject panelDie;
    [SerializeField] private AudioClip[] audioClips;
    public void Die()
    {
        //PlayerController.Instance.myAnimator.Play("Die");
        IsDeath = true;        
       this.transform.GetComponent<SpriteRenderer>().enabled = false;
        this.transform.GetComponent<CapsuleCollider2D>().size = new Vector2(0.1f, 0.1f);
        GameObject plDie = Instantiate(playerDie, this.transform.position,Quaternion.identity);
        SpriteRenderer spr= plDie.GetComponent<SpriteRenderer>();
        spr.DOFade(0, 4f).OnComplete(()=>Destroy(plDie));
        //rb.velocity=Vector2.zero;
        StartCoroutine(DieRoutine());
        SoundFXManagement.Instance.PlaySoundFXClip(audioClips[1], this.transform, 1f);
    }
   IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(.5f);
        this.transform.gameObject.SetActive(false);
        //yield return new WaitForSeconds(.5f);
       // panelDie.gameObject.SetActive(true);

    }    
    public void TakeDamage(float damage)
    {
        if(currentHealth>0)
        {
            SoundFXManagement.Instance.PlaySoundFXClip(audioClips[0], this.transform, 1f);
            CameraShake.instance.ShakeCamera(cameraShakeForce);
            currentHealth -= damage;
            StartCoroutine(flash.FlashRoutine());
            PlayerController.Instance.myAnimator.SetTrigger(AnimationString.Hurt);
        }    
    }    
   public void BatTu()
    {
        currentHealth += 100000;
    }    
    public void hackvang()
    {
        currentCoin += 1000000;
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
      
        
        
        flash=GetComponent<Flash>();
            
        rb =GetComponent<Rigidbody2D>();      
        StartCoroutine(RefreshStaminaRoutine());

       // playerData.playerMaxDamage = playerData.playeMaxLevel * itemDamage.howMuchMore;
    }
    private void Update()
    {
        if (currentHealth <= 0 && !IsDeath)
        {
            Die();
            CameraZoom.instance.ZoomIn();
            TitlePopup.instance.StartGameoverText();
        }
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
  public  IEnumerator RefreshHealthRoutine( float maxHealthCanRefersh)
    {   float count = 0;
        while (count <= maxHealthCanRefersh)
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
