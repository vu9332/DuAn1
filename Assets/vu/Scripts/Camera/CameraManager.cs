using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCamera;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;

    public float fallSpeedYDampingChangeThreshold = -15f;
    public bool IsLerpingYDamping { get; private set; }
    public bool LerpededFromPlayerFalling { get; set; }

    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineVirtualCamera _currentCamera;
    private Coroutine _lerpYPanCoroutine;
    private Coroutine _panCameraCoroutine;
    private float _normYPanAmount;

    private Vector2 _statingTrackedObjectOffSet;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < _allVirtualCamera.Length; i++)
        {
            if (_allVirtualCamera[i].enabled)
            {
                _currentCamera = _allVirtualCamera[i];
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            }
        }

        _statingTrackedObjectOffSet = _framingTransposer.m_TrackedObjectOffset;

    }
    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
        Debug.Log("Do coroutine");
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {


        IsLerpingYDamping = true;

        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpededFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }
        float elapsedTime = 0;
        while (elapsedTime < _fallYPanTime)
        {

            elapsedTime += Time.deltaTime;
            float lerpPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallYPanTime));
            _framingTransposer.m_YDamping = lerpPanAmount;
            yield return null;
        }
        IsLerpingYDamping = false;
    }
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));

    }
    IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingtPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 starttingPos = Vector2.zero;
        if (!panToStartingtPos)
        {
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.left;
                    break;
            }
            endPos*=panDistance;
            starttingPos = _statingTrackedObjectOffSet;
            endPos += starttingPos;
        }
        else
        {
            starttingPos=_framingTransposer.m_TrackedObjectOffset;
            endPos = _statingTrackedObjectOffSet;
        }

        float elapsedTime = 0f;
        while (elapsedTime<panTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 panLerp=Vector3.Lerp(starttingPos, endPos, (elapsedTime / panTime));     
       _framingTransposer.m_TrackedObjectOffset = panLerp;
            yield return null;
        }
    }
    public void SwapCamera(CinemachineVirtualCamera cameraFormLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection)
    {
        if (_currentCamera == cameraFormLeft && triggerExitDirection.x > 0)
        {
            cameraFromRight.enabled = true;
            cameraFormLeft.enabled = false;

            _currentCamera = cameraFromRight;
            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0)
        {
            cameraFromRight.enabled = false;
            cameraFormLeft.enabled = true;

            _currentCamera = cameraFormLeft;
            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }
}
