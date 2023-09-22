using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private WeaponStatusSO qwer;
    [SerializeField] private float moveSec;
    private Camera _cam;
    private Rigidbody2D _rig2D;

    Vector3 _dir = Vector3.zero;

    private void Awake()
    {
        _cam = Camera.main;
        _rig2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _dir = _cam.ScreenToWorldPoint(Input.mousePosition);
            _dir.z = 0;
            DOTween.Kill(gameObject);
            transform.DOMove(_dir, moveSec);
        }
        
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        //_rig2D.velocity = new Vector2(x,y)*3;

    }
}
