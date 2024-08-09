using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
   // private float shakeIntensity = 5f;
    private float shakeTime = 0.2f;

    private float timer = 0;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    [SerializeField] private Transform cameraTransform;
    private void Awake()
    {
        cinemachineVirtualCamera=GetComponent<CinemachineVirtualCamera>();

        if(instance == null )
        {
            instance = this;

        }
    }
    private void Start()
    {
        StopShake();
    }
    public void ShakeCamera(float shakeIntensity)
    {
        CinemachineBasicMultiChannelPerlin _cbmcp2=cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp2.m_AmplitudeGain=shakeIntensity;
         timer = shakeTime;
    }
    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp2=cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp2.m_AmplitudeGain = 0f;
        timer = 0f;

        Vector3 rotation = cameraTransform.rotation.eulerAngles;
        rotation.z = 0;
        cameraTransform.rotation = Quaternion.Euler(rotation);

    }
    private void Update()
    {
        if (timer>0)
        {
            timer -= Time.deltaTime;
            if (timer<=0)
            {
                StopShake();
            }
        }
    }
}
