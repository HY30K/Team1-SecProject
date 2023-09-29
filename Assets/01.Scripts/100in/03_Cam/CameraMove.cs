using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera _mainCam;
    private float _camSize;
    public float ScrollSpeed { get; set; }
    private void Awake()
    {
        _mainCam = Camera.main;
        _camSize = _mainCam.orthographicSize;
        ScrollSpeed = 50f;
    }
    private void Update()
    {
        _camSize += -Input.mouseScrollDelta.y * ScrollSpeed * Time.deltaTime;
        _camSize = Mathf.Clamp(_camSize, 3f, 10f);
        _mainCam.orthographicSize = Mathf.Clamp(_camSize, 3f, 10f);
    }


}
