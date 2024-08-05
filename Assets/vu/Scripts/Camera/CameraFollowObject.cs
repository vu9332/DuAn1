using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float _flipYRotationTime = 0.5f;
    private PlayerController player;

    private Coroutine _turnCoroutine;

    private bool _isFacingRight;
    private void Awake()
    {
        player = playerTransform.GetComponent<PlayerController>();
        _isFacingRight=player.IsFacingRight;
    }
    private void Update()
    {
        this.transform.position=playerTransform.transform.position;
    }
    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
       
    }
    private IEnumerator FlipYlerp()
    {
        float startRotation=transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / _flipYRotationTime));
            transform.rotation=Quaternion.Euler(0f,yRotation,0f);
           yield return null;
        }
    }
    private float DetermineEndRotation()
    {
        _isFacingRight=!_isFacingRight;
        if (_isFacingRight)
            return 10f;
        else return 0f;
    }
}
