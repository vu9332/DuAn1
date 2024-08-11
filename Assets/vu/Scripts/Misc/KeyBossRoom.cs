using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBossRoom : PowerUp
{
    [SerializeField] private PlayerData newData;
    [SerializeField] private GameObject imageKeyObject;
    private RectTransform posEnd;
    protected override void Awake()
    {
        posEnd = GameObject.Find("KeyImage").GetComponent<RectTransform>();
        base.Awake();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            newData.IsKeyUnlock = true;
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_coin);
            rb.bodyType = RigidbodyType2D.Kinematic;
            coll.enabled = false;
            base.OnAniamtionCoins(transform, imageKeyObject, posEnd);
            Destroy(gameObject);
        }
        if (coll.IsTouchingLayers(groundLayer))
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_drop_key);
        }
    }
}
