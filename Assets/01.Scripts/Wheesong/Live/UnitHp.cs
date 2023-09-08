using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHp : Living
{
    protected override void Die()
    {
        base.Die();
        WaveSystem.Instance.DieEnemy();
        PoolingManager.Instance.Push(gameObject);
    }
}
