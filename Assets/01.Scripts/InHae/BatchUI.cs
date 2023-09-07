using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BatchUI : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler 
{
    private string currentUnitName;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.UnitBatch(mousePos, currentUnitName);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.UnitAlphaBatch(mousePos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentUnitName = eventData.pointerCurrentRaycast.gameObject.name;
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.UnitCreate(mousePos, currentUnitName+"Alpha");
    }
}
