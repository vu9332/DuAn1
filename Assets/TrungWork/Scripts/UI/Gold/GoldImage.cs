using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldImage : PowerUpImage
{
    private RectTransform imageCoinRectPlayer;
    private void Awake()
    {
        imageCoinRectPlayer = GameObject.Find("CoinImage").GetComponent<RectTransform>();
    }
    private void Update()
    {
        OnAnimation(); 
    }
    private void OnAnimation()
    {
        if (!isTouchingImage)
        {
            base.PlayAnimation(imageCoinRectPlayer, imagePowerUpRect);
            if (isTouchingImage)
            {
                playerData.playerCoin = ++playerData.playerCoin;
                CharacterEvents.characterTookItem.Invoke(UIManager.UIManagerInstance.coinTextPrefab, GameObject.Find("Player"), 1);
                Destroy(gameObject, 2f);
            }
        }
    }
}
