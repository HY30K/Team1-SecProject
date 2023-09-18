using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Living : MonoBehaviour
{
    [Header("HpBar")]
    [SerializeField] private Transform hpBarPrefab;
    [SerializeField] float hpbarPosX = 1f;

    private GameObject hpBar;
    private Transform sliderValue;
    private Rigidbody2D rb;

    protected float hp;
    protected float maxHp;
    public bool isDie;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        hpBar = PoolingManager.Instance.Pop(hpBarPrefab.name, transform.position + new Vector3(0, hpbarPosX), transform);
        sliderValue = hpBar.transform.GetChild(0);
        isDie = false;
    }

    protected virtual void Update()
    {
        sliderValue.localScale = new Vector3(hp / maxHp, 1, 0);
    }

    public void HpSetting(float setHp)
    {
        maxHp = setHp;
        hp = maxHp;
    }

    public virtual void OnHit(float dmg)
    {
        hp -= dmg;

        if (hp <= 0 && !isDie)
        {
            isDie = true;
            Die();
        }
    }

    public virtual void Die()
    {
        rb.velocity = Vector2.zero;
        PoolingManager.Instance.Push(hpBar);
    }
}
