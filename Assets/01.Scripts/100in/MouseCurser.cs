using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCurser : MonoBehaviour
{
    private Camera _cam;
    private Vector2 _dir = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _cam = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _dir = _cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _dir;

    }

    public void SpritSet(Sprite asdf)
    {
        _spriteRenderer.sprite = asdf;
    }
}
