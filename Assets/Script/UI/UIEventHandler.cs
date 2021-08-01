using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
{
    public Action<PointerEventData> ClickHandlerBucket = null;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그시작!");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("드래그끝!");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ClickHandlerBucket != null)
        {
            ClickHandlerBucket.Invoke(eventData);
        }
    }
}
