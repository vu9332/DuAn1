using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyImage : PowerUpImage
{
    private static bool _needKey=true;
    public static bool NeedKey { get { return _needKey; } set { _needKey = value; } }


    private RectTransform imageKeyRectPlayer;
    private float width,height;
    private void Awake()
    {
        imageKeyRectPlayer = GameObject.Find("KeyImage").GetComponent<RectTransform>();
    }
    private void Start()
    {
        width = imageKeyRectPlayer.sizeDelta.x;
        height = imageKeyRectPlayer.sizeDelta.y;
        imagePowerUpRect.sizeDelta = new Vector2(width, height);
    }
    private void Update()
    {
        OnAnimation();
    }
    private void OnAnimation()
    {
        if (!isTouchingImage)
        {
            base.PlayAnimation(imageKeyRectPlayer, imagePowerUpRect);
        }
        if(!_needKey)
        {
            this.gameObject.SetActive(false);
        }
    }
}
