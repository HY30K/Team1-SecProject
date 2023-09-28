using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("MouseWheel")]
    [SerializeField] private float wheelSpeed;
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;

    private CinemachineVirtualCamera vcam;
    private Rigidbody2D rb;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MouseWheel();
    }

    private void MouseWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * -wheelSpeed;

        if (vcam.m_Lens.OrthographicSize >= maxSize && scroll > 0) //√÷¥Î ¡‹æ∆øÙ
            vcam.m_Lens.OrthographicSize = maxSize;
        else if (vcam.m_Lens.OrthographicSize <= minSize && scroll < 0) //√÷¥Î ¡‹¿Œ
            vcam.m_Lens.OrthographicSize = minSize;
        else
            vcam.m_Lens.OrthographicSize += scroll;
    }
}
