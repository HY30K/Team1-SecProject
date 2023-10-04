using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private WeaponStatusSO qwer;
    [SerializeField] private float moveSpeed;
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
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _rig2D.velocity = new Vector2(x, y) * moveSpeed;

    }
}
