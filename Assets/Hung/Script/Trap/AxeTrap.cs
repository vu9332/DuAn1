using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AxeTrap : MonoBehaviour
{
    public Transform pivot; // Điểm cố định để quả lắc đung đưa quanh
    public float angle = 45f; // Góc tối đa của đung đưa (độ)
    public float speed = 2f; // Tốc độ đung đưa

    
    private float currentAngle;
    private float time;

    void Start()
    {
        
        // Chuyển góc từ độ sang radian
        currentAngle = angle * Mathf.Deg2Rad;
    }

    void Update()
    {
        // Tăng thời gian để tạo ra chuyển động đung đưa
        time += Time.deltaTime * speed;

        // Tính toán góc hiện tại theo hàm Sin để tạo hiệu ứng đung đưa
        float angleOffset = Mathf.Sin(time) * angle;

        // Cập nhật vị trí của đối tượng dựa trên góc
        Vector3 offset = new Vector3(Mathf.Sin(angleOffset * Mathf.Deg2Rad), -Mathf.Cos(angleOffset * Mathf.Deg2Rad), 0) * (pivot.position - transform.position).magnitude;
        transform.position = pivot.position + offset;

        // Đặt góc quay của đối tượng
        transform.rotation = Quaternion.Euler(0, 0, angleOffset);
    }

}
