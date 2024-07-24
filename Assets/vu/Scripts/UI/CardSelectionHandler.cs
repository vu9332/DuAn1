using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISelectHandler,IDeselectHandler
{
    [SerializeField] private float verticalMoveAmount = 30f;
    [SerializeField] private float moveTime = .1f;
    [Range(0f, 2f),SerializeField] private float scaleAmount = 1.1f;

    Vector3 startPos;
    Vector3 startScale;

    private void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
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
                endPos = startPos + new Vector3(0, verticalMoveAmount, 0);
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
