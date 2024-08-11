using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PowerUp
{
    [SerializeField] private GameObject imageKeyObject;
    private RectTransform posEnd;

    protected override void Awake()
    {
        base.Awake();
        posEnd = GameObject.Find("CoinImage").GetComponent<RectTransform>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_coin);
            rb.bodyType = RigidbodyType2D.Kinematic;
            coll.enabled = false;
            base.OnAniamtionCoins(transform, imageKeyObject, posEnd);
            Destroy(gameObject);
        }
        if (coll.IsTouchingLayers(groundLayer))
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_coin);
        }
    }
}
