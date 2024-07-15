using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class parallaxController : MonoBehaviour
{
    Transform cam; //Camera chinh
    Vector3 camStarPos; //vi tri camera
    float distanceX; // khoang cach giua vi tri camera bat dau va camera hien tai
    float distanceY;

    GameObject[] backGrounds;
    Material[] mat;
    float[] backspeed;

    float farthestBack;

    [Range(0.01f,0.05f)]
    public float parallaxSpeed;

    void Start()
    {
        cam = Camera.main.transform;
        camStarPos = cam.position;

        int backcount = transform.childCount;
        mat = new Material[backcount];
        backspeed = new float[backcount];
        backGrounds = new GameObject[backcount];

        for (int i = 0; i < backcount; i++)
        {
            backGrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backGrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backcount);
    }

    void BackSpeedCalculate (int backcount)
    {
        for (int i = 0; i < backcount; i++)
        {
            if (backGrounds[i].transform.position.z - camStarPos.z >farthestBack)
            {
                farthestBack = backGrounds[i].transform .position.z - cam.position.z;

            }
        }

        for (int i = 0;i < backcount; i++)
        {
            backspeed[i] = 1 - (backGrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distanceX = cam.position.x - camStarPos.x;
        distanceY = cam.position.y - camStarPos.y;
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);
        for (int i = 0;i < backGrounds.Length ; i++)
        {
            float speed = backspeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX,distanceY)*speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
