using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class BatchUI : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image[] uguis;
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
        Debug.Log(currentUnitName);
        BatchManager.Instance.UnitBatch(mousePos, currentUnitName);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.UnitAlphaBatch(mousePos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (Image ui in uguis)
        {
            Color newColor = ui.color;
            newColor.a = 0.4f;
            ui.color = newColor;
        }
        currentUnitName = eventData.pointerCurrentRaycast.gameObject.name.Replace("_Image", "");
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.UnitCreate(mousePos, currentUnitName+"Alpha");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (Image ui in uguis)
        {
            Color newColor = ui.color;
            newColor.a = 1;
            ui.color = newColor;
        }
        BatchManager.Instance.UnitDestroy();
    }
}
