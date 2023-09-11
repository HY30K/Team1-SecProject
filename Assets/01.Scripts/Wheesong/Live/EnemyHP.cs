using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : Living
{
    private Enemy enemy;

    protected override void Die()
    {
        base.Die();
        enemy = GetComponent<Enemy>();
        enemy.state = State.DIE;
        //죽는 애니메이션 추가

        StartCoroutine(PushEnemy(1));
    }

    IEnumerator PushEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        WaveSystem.Instance.DieEnemy();
        PoolingManager.Instance.Push(gameObject);
    }
}
