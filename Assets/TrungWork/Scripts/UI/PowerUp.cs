using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration=1f;
    [SerializeField] protected Ease easeCoin;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected PowerUpSC powerUpSC;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected virtual void Awake()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void OnAniamtionCoins(Transform positionPowerUp,GameObject imagePowerUp,RectTransform posEnd)
    {
        Vector2 posCoinsInOverlay = Camera.main.WorldToScreenPoint(positionPowerUp.position);
        GameObject imagePowerUpObj = Instantiate(imagePowerUp, posCoinsInOverlay, Quaternion.identity);
        imagePowerUpObj.GetComponentInChildren<Image>().rectTransform.position = posCoinsInOverlay;
        imagePowerUpObj.GetComponentInChildren<Image>().rectTransform.DOMove(posEnd.position, duration).SetEase(easeCoin);
    }
}
