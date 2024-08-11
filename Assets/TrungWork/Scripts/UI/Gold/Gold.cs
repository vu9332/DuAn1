using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;

public class Gold : PowerUp
{
    [SerializeField] private GameObject imageCoinsObject;
    [SerializeField] private RectTransform posEnd;
    
    protected override void Awake()
    {
        base.Awake();
        posEnd = GameObject.Find("CoinImage").GetComponent<RectTransform>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_pick_up_1);
            rb.bodyType = RigidbodyType2D.Kinematic;
            coll.enabled = false;
            base.OnAniamtionCoins(transform, imageCoinsObject,posEnd);
            Destroy(gameObject);
        }
        if (coll.IsTouchingLayers(groundLayer))
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_coin);
        }
    }
}
