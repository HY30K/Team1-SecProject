using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] WeaponStatusSO _weaponStatus;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer.sprite = _weaponStatus.WeaponSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }
}
