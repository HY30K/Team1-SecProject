using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHp : Living
{
    private Unit unit;

    protected override void Die()
    {
        base.Die();
        unit = GetComponent<Unit>();
        unit.state = State.DIE;
        //�״� �ִϸ��̼� �߰�

        StartCoroutine(PushUnit(1));
    }

    IEnumerator PushUnit(float time)
    {
        yield return new WaitForSeconds(time);
        WaveSystem.Instance.DieUnit();
        PoolingManager.Instance.Push(gameObject);
    }
}
