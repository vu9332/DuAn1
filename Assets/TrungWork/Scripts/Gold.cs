using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 posTarget;
    private Vector3 gameObjectPos;
    private bool isCollectedCoin=false;
    private Rigidbody2D rb;
    private Collider2D coll;
    private void Awake()
    {
        coll=GetComponent<Collider2D>();
        rb=GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        RectTransform rectPosition= GameObject.Find("CoinImage").GetComponent<RectTransform>();
        posTarget = rectPosition.position;
        gameObjectPos= Camera.main.WorldToScreenPoint(transform.position);
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
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_pick_up);
            playerData.playerCoin = ++playerData.playerCoin;
            isCollectedCoin=true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            coll.enabled = false;
            Destroy(gameObject);
        }
        if (coll.IsTouchingLayers(groundLayer))
        {
            AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_coin);
        }
    }
}
