using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCamera;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallPanTime = 0.35f;

    public float fallSpeedYDampingChangeThreshold = -15f;
    public bool IsLerpingYDamping {  get; private set; }    
    public bool LerpededFromPlayerFalling { get;set; }

    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineVirtualCamera _currentCamera;
    private Coroutine _lerpYPanCoroutine;
    private float _normYPanAmount;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < _allVirtualCamera.Length; i++)
        {
            if(_allVirtualCamera[i].enabled )
            {
                _framingTransposer=_currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

    }
    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine=StartCoroutine(LerpYAction(isPlayerFalling));
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpededFromPlayerFalling=true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }
        float elapsedTime = 0;
        while (elapsedTime < _fallPanTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpPanAmount= Mathf.Lerp(startDampAmount, endDampAmount,( elapsedTime/_fallPanTime));
            _framingTransposer.m_YDamping=lerpPanAmount;
            yield return null;
        }
        IsLerpingYDamping =false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
