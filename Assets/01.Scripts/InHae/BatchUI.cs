using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class BatchUI : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private string currentUnitName;
    private Camera mainCam;
    private RaycastHit2D hit;

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
        Debug.Log("log");
        currentUnitName = eventData.pointerCurrentRaycast.gameObject.name;
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.UnitCreate(mousePos, currentUnitName+"Alpha");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        BatchManager.Instance.UnitDestroy();
    }
}