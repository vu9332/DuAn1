using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour, IDamageAble
{
    [Header("key Instantiate")]
    [SerializeField] private float chestHealth;
    [SerializeField] private float keyJumpHeight;
    [SerializeField] private float keyJumpDoration;
    [SerializeField] private Transform keySpawnPoint;
    [SerializeField] private GameObject keyRoom;
    [Header("")]
    [SerializeField] private float time;
    [SerializeField] private bool isKey=false;
    private float timer = 0;
    [SerializeField] private AudioClip[] audioClip;
    public float health { get { return chestHealth; } set { chestHealth = value; } }
    string text = "The Demon King has taken notice of you";
    public void Die()
    {
        if (!isKey)
        {
            SoundFXManagement.Instance.PlaySoundFXClip(audioClip[1], this.transform, 1f);
            StartCoroutine(JumpKey());
            MessageManager.istance.StartPopUp(text, 4f);
        } 
            
    }
    IEnumerator JumpKey()
    {
        GameObject go = Instantiate(keyRoom,keySpawnPoint.position,Quaternion.identity);
        go.transform.DOMoveY(go.transform.position.y+keyJumpHeight,keyJumpDoration).SetEase(Ease.OutCirc);
        isKey = true;
        timer = time;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (timer <= 0)
        {
        //  go.gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
           // go.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }    
    
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            SoundFXManagement.Instance.PlaySoundFXClip(audioClip[0],this.transform,1f);
            ChestShake();
        }
        else if(health <= 0)
        {
           
            Animator animator = GetComponent<Animator>();
            animator.Play("Chest");
            health = 0;
            Die();
        }
    }

    void ChestShake()
    {
        Vector3 originTransform=this.transform.position;
        //transform.DOKill();
        this.transform.DOShakePosition(.2f,1f,20,90,false,true).OnKill(()=>transform.position=originTransform);

    }
}
