using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom instance;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float zoomIn;
    [SerializeField] private float duration;

    private float originalSize;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        originalSize=virtualCamera.m_Lens.OrthographicSize;
    }
    public void ZoomIn()
    {
        StatusManagement.PressHideUI();
        DOTween.To(()=>virtualCamera.m_Lens.OrthographicSize,x=>virtualCamera.m_Lens.OrthographicSize=x,zoomIn,duration).SetEase(Ease.InOutSine);
    }
    public void ZoomOut()
    {
        StatusManagement.PressShowUI();
        DOTween.To(()=>virtualCamera.m_Lens.OrthographicSize,x=>virtualCamera.m_Lens.OrthographicSize=x,originalSize,.2f).SetEase(Ease.OutExpo);
    }
}
