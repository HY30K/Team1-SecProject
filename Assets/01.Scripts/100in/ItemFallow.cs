using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ItemFallow : MonoBehaviour
{
    public Transform Trm;

    private VisualElement m_Bar;
    private UIDocument _dot;
    private Camera m_MainCamera;

    private void Awake()
    {
        _dot = GetComponent<UIDocument>();
    }
    private void Start()
    {
        m_MainCamera = Camera.main;
        m_Bar = _dot.rootVisualElement.Q<VisualElement>("Container");
        //_dot.
        //SetPosition();

    }

    private void OnMouseDrag()
    {
        Debug.Log("dwf");
    }
    public void SetPosition()
    {
        Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(m_Bar.panel, Trm.position, m_MainCamera);
        //Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(m_Bar.panel, m_MainCamera.ScreenToWorldPoint(Input.mousePosition), m_MainCamera);

        m_Bar.transform.position = newPosition.WithNewX(newPosition.x - m_Bar.layout.width / 2);
    }
    private void LateUpdate()
    {
        if(Trm is not null)
        {
            SetPosition();
            
        }
    }
}

public static class Test
{
    public static Vector2 WithNewX(this Vector2 vector, float newX) => new Vector2(newX, vector.y);
}
    