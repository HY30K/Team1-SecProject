using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("���� SO�� �־��ֽʽÿ�")]
    [SerializeField] WeaponStatusSO _weaponStatus;

    private SpriteRenderer _spriteRenderer;
    private ItemWaitLounge _waitLounge;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _weaponStatus.WeaponSprite;
        _waitLounge = GameObject.Find("ItemManager").GetComponent<ItemWaitLounge>();
    }

    private void Update()
    {
        _spriteRenderer.material.SetTexture("Sprite", _spriteRenderer.sprite.texture);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&_waitLounge.TakeItemChake())
        {
            _waitLounge.TakeItem(_weaponStatus);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("������â ��á��");
        }
    }

    private void OnMouseDown()
    {
        if (_waitLounge.TakeItemChake())
        {
            _waitLounge.TakeItem(_weaponStatus);
            PoolingManager.Instance.Push(gameObject);
        }
    }
}
