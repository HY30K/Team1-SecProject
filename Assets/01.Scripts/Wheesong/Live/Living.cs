using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    [Header("HpBar")]
    [SerializeField] private Transform hpBarPrefab;
    private GameObject hpBar;
    private Transform sliderValue;

    const float hpbarPosX = 1f;
    public float hp;
    protected float maxHp;

    protected virtual void OnEnable()
    {
        maxHp = hp;
        hpBar = PoolingManager.Instance.Pop(hpBarPrefab.name, transform.position + new Vector3(0, hpbarPosX), transform);
        sliderValue = hpBarPrefab.GetChild(0);
    }

    protected virtual void Update()
    {
        sliderValue.localScale = new Vector3(hp / maxHp, 1);
    }

    public virtual void OnHit(float dmg)
    {
        hp -= dmg;
    }

    protected virtual void Die()
    {
        PoolingManager.Instance.Push(hpBar);
    }
}
