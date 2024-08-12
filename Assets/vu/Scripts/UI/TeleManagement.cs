using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleManagement : MonoBehaviour
{
    public static TeleManagement Instance { get; set; }
    [SerializeField] private GameObject gateEnd;
    
    SpriteRenderer ima;
    void Start()
    {
        gateEnd.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenGateEnd()
    {
        gateEnd.gameObject.SetActive(true);
       
        ima =gateEnd.gameObject.GetComponent<SpriteRenderer>();
        ima.color=new Color(ima.color.r,ima.color.g,ima.color.b,0);
        StartCoroutine(DoGate());
    }
    IEnumerator DoGate()
    {
        yield return new WaitForSeconds(1f);
        ima.DOFade(1, 2f);
    }
}
