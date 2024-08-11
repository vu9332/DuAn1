using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpImage : MonoBehaviour
{
    [SerializeField] protected PlayerData playerData;
    [SerializeField] protected RectTransform imagePowerUpRect;
    protected bool isTouchingImage = false;
    protected void PlayAnimation(RectTransform imageCoinUIPlayer, RectTransform imagePowerUpRect)
    {
        if (imageCoinUIPlayer.position == imagePowerUpRect.position)
        {
            isTouchingImage = true;
        }
    }
}
