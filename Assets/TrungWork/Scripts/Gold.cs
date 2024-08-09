using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;

public class Gold : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject imageCoinsObject;
    [SerializeField] float duration;
    [SerializeField] private Ease easeCoin;
    private Transform posEnd;
    private Vector3 posTarget;
    private Vector3 gameObjectPos;
    private bool isCollectedCoin=false;
    private Rigidbody2D rb;
    private Collider2D coll;
    private void Awake()
    {
        posEnd = GameObject.Find("CoinImage").GetComponent<RectTransform>();
        coll=GetComponent<Collider2D>();
        rb=GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isCollectedCoin)
        {
            transform.position = Vector3.Lerp(gameObjectPos, posTarget, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            CharacterEvents.characterTookItem.Invoke(UIManager.UIManagerInstance.coinTextPrefab, gameObject, 1);
            playerData.playerCoin = ++playerData.playerCoin;
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_pick_up);
            isCollectedCoin=true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            coll.enabled = false;
            OnAniamtionCoins();
            Destroy(gameObject);
        }
        if (coll.IsTouchingLayers(groundLayer))
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_coin);
        }
    }
    public void OnAniamtionCoins()
    {
        Vector2 posCoinsInOverlay=Camera.main.WorldToScreenPoint(transform.position);
        GameObject coinImage=Instantiate(imageCoinsObject, posCoinsInOverlay, Quaternion.identity);
        coinImage.GetComponentInChildren<Image>().rectTransform.position = posCoinsInOverlay;
        coinImage.GetComponentInChildren<Image>().rectTransform.DOMove(posEnd.position, duration).SetEase(easeCoin);
        if(coinImage.GetComponentInChildren<Image>().rectTransform== posEnd)
        {
            Destroy(coinImage);
        }
    }
}
