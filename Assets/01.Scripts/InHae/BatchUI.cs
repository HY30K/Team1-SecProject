using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BatchUI : MonoBehaviour, IPointerClickHandler
{
    private Dictionary<string, Image> units = new Dictionary<string, Image>();
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            units.Add(transform.GetChild(i).name, transform.GetChild(i).GetComponent<Image>());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Ray ray = mainCam.ScreenPointToRay(eventData.position);
        
        
    }
}
