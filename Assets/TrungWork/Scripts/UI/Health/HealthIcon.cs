using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIcon : PowerUp
{
    [SerializeField] private PlayerData playerData;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_pick_up_2);
            rb.bodyType = RigidbodyType2D.Kinematic;
            coll.enabled = false;
            playerData.playerCurrentHealth += powerUpSC.amountPlayerContain;
            Destroy(gameObject);
        }
    }
}
