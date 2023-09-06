using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rig2D;
    [SerializeField] private WeaponStatusSO qwer;
    private void Awake()
    {
        _rig2D = GetComponent<Rigidbody2D>();
        Debug.Log(qwer.qwer);
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _rig2D.velocity = new Vector2(x,y)*3;
    }
}
