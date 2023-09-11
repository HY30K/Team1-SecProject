using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : Living
{
    protected override void Die()
    {
        base.Die();
        WaveSystem.Instance.DieEnemy();
        PoolingManager.Instance.Push(gameObject);
    }
}
