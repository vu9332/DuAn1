using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFollowGameObject : MonoBehaviour
{
    public GameObject objHealthBar;
    public GameObject obj;
    // Tham chiếu tới Image trong Canvas
    public Image targetImage1;
    public Image targetImage2;

    // Tham chiếu tới Camera chính
    private Camera mainCamera;

    void Start()
    {
        // Lấy tham chiếu tới Camera chính
        mainCamera = Camera.main;
        objHealthBar.SetActive(true);
    }

    void Update()
    {
        if (obj != null && targetImage1 != null && targetImage2 != null)
        {
            // Lấy vị trí của GameObject mục tiêu trong không gian màn hình
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(obj.transform.position) + new Vector3(0,120f,0);

            // Cập nhật vị trí của Image để khớp với vị trí trong không gian màn hình của GameObject mục tiêu
            targetImage1.rectTransform.position = screenPosition;
            targetImage2.rectTransform.position = screenPosition;
        }
        if (obj == null)
        {
            objHealthBar.SetActive(false);
            Destroy(gameObject);
        }
    }
}
