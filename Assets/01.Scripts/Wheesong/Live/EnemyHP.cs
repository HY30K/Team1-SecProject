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
        //�״� �ִϸ��̼� �߰�

        StartCoroutine(PushEnemy(1));
    }

    IEnumerator PushEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        WaveSystem.Instance.DieEnemy();
        PoolingManager.Instance.Push(gameObject);
    }
}
