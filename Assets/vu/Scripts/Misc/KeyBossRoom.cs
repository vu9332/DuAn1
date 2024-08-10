using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBossRoom : MonoBehaviour
{
    [SerializeField] private PlayerData newData;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent <PlayerController >()!=null)
        {
            newData.IsKeyUnlock = true;
           SpriteRenderer im = this.gameObject.GetComponent<SpriteRenderer>();
            im.DOFade(0f, 2);
        }
    }
}
