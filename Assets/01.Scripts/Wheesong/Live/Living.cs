using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Living : MonoBehaviour
{
    [Header("HpBar")]
    [SerializeField] private Transform hpBarPrefab;
    private GameObject hpBar;
    private Transform sliderValue;

    const float hpbarPosX = 1f;
    protected float hp;
    protected float maxHp;
    public bool isDie;

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
        PoolingManager.Instance.Push(hpBar);
    }
}
