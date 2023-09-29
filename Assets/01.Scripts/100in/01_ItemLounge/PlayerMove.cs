using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{
    private Camera _cam;
    Vector3 _dir = Vector3.zero;
    private void Awake()
    {
        _cam = Camera.main;
    }
    private void Update()
    {
        Moveing();
    }
 
    private void Moveing()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _dir = _cam.ScreenToWorldPoint(Input.mousePosition);
            _dir.z = 0;
            transform.DOMove(_dir, 1);
        }
    }
}
