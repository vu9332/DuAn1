using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISelectHandler,IDeselectHandler
{
    [SerializeField] private float verticalMoveAmount = 30f;
    [SerializeField] private float moveTime = .1f;
    [Range(0f, 2f),SerializeField] private float scaleAmount = 1.1f;


    [SerializeField] private float horizontalMoveAmount = 30f;
    [SerializeField] private float horizontalVoveTime = .1f;
    [Range(0f, 2f),SerializeField] private float horizontalScaleAmount = 1.1f;

    Vector3 startPos;
    Vector3 startScale;

    [SerializeField] public string cardName;
    [SerializeField] public bool isSelected=false;
    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        startPos =this.transform.position;
        startScale =this.transform.localScale;
    }
    
    public void Press()
    {
       
        switch (cardName)
        {
            case "Skill 1":
                {
                    if (playerData.playerCoin >= 10&&playerData.playerLevel>=2)
                    {
                        isSelected = true;
                        this.gameObject.SetActive(false);
                        SkillManager.Instance.UnLockSkill(cardName);
                        PlayerHealth.Instance.UseCoin(5);
                    }
                    else StartCoroutine(MoveCardHorizontal(true));

                    
                }                 
                    break;                
            case "Skill 2":
                if (playerData.playerCoin >= 10 && playerData.playerLevel >= 3)
                {
                    isSelected = true;
                    this.gameObject.SetActive(false);
                    SkillManager.Instance.UnLockSkill(cardName);
                    PlayerHealth.Instance.UseCoin(5);

                }
                else StartCoroutine(MoveCardHorizontal(true));
                break;
            case "Skill 3":
                if (playerData.playerCoin >= 10 && playerData.playerLevel >= 5)
                {
                    isSelected = true;
                    this.gameObject.SetActive(false);
                    SkillManager.Instance.UnLockSkill(cardName);
                    PlayerHealth.Instance.UseCoin(5);

                }
                else StartCoroutine(MoveCardHorizontal(true));

                break;
        }
       // this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        switch (cardName)
        {
            case "Skill 1":
                {
                    if ( playerData.playerLevel >= 2&&playerData._isSkillOneUnlock)
                    {

                        this.gameObject.SetActive(false);
                    }
                    else
                    {
                        this.gameObject.SetActive(true);
                        Debug.Log("You Dont have a coin");
                    }

                }
                break;
            case "Skill 2":
                if (playerData.playerLevel >= 3 && playerData._isSkillTwoUnlock)
                {
                    this.gameObject.SetActive(false);

                }
                else
                {
                    this.gameObject.SetActive(true);
                Debug.Log("You Dont have a coin");
                }
                break;
            case "Skill 3":
                if ( playerData.playerLevel >= 5&& playerData._isSkillThreeUnlock)
                {
                    this.gameObject.SetActive(false);

                }
                else
                {
                    this.gameObject.SetActive(true);
                    Debug.Log("You Dont have a coin");
                }

                break;
        }
    }
    private IEnumerator MoveCard(bool startingAnmation)
    {
        Vector3 endPos;
        Vector3 endScale;

        float eclapsedTime = 0f;
        while (eclapsedTime < moveTime)
        {
            eclapsedTime += Time.deltaTime;

            if (startingAnmation)
            {
                endPos = startPos + new Vector3(0,verticalMoveAmount, 0);
                endScale = startScale * scaleAmount;
            }
            else
            {
                endPos = startPos;
                endScale = startScale;
            }
            Vector3 lerpPos= Vector3.Lerp(this.transform.position,endPos,(eclapsedTime/moveTime));
            Vector3 lerpScale=Vector3.Lerp(this.transform.localScale,endScale,(eclapsedTime/moveTime));

            this.transform.position = lerpPos;
            this.transform.localScale = endScale;
            yield return null;
        }
    }
     private IEnumerator MoveCardHorizontal(bool startingAnmation)
    {
        Vector3 endPos;
        Vector3 endScale;

        float eclapsedTime = 0f;
        while (eclapsedTime < moveTime)
        {
            eclapsedTime += Time.deltaTime;

            if (startingAnmation)
            {
                endPos = startPos + new Vector3(0,-horizontalMoveAmount,0);
                endScale = startScale * horizontalScaleAmount;
            }
            else
            {
                endPos = startPos;
                endScale = startScale;
            }
            Vector3 lerpPos= Vector3.Lerp(this.transform.position,endPos,(eclapsedTime/ horizontalVoveTime));
            Vector3 lerpScale=Vector3.Lerp(this.transform.localScale,endScale,(eclapsedTime/ horizontalVoveTime));

            this.transform.position = lerpPos;
            this.transform.localScale = endScale;
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));
    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
       StartCoroutine (MoveCard(false));
    }
  
}
