using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActive : Unit
{
    bool canAttack = true;

    const float whiteFrame = 0.07f;

    protected override void OnEnable()
    {
        base.OnEnable();
        canAttack = true;
    }

    protected override void Idle()//������ �ֱ�
    {
        
    }

    protected override void Chase()//�� �ѱ�
    {
        Vector2 dir = (enemyTrs.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }
    
    protected override void Attack()//����
    {
        if (!canAttack) return;

        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    protected override void Die()//������ ���ٰ�(���� ����)
    {
        col.enabled = false;
    }

    public void OnHit()
    {
        StartCoroutine(OnHitting());
    }

    public void OnAggro(Transform enemy)
    {
        if (state != State.IDLE) return;

        state = State.CHASE;
        enemyTrs = enemy;
    }

    private void OnDisable()
    {
        canAttack = false;
        StopAllCoroutines();
    }

    private IEnumerator AttackDelay()
    {
        enemyTrs.GetComponent<EnemyActive>().OnHit();
        yield return new WaitForSeconds(attackDelay);
        if(enemyTrs != null && DetectionLength(attackRange))
            enemyTrs.GetComponent<EnemyHP>().OnHit(attack);

        canAttack = true;
    }

    private IEnumerator OnHitting()
    {
        sp.material.SetFloat("WhiteFrame", 1);
        yield return new WaitForSeconds(whiteFrame);
        sp.material.SetFloat("WhiteFrame", 0);
    }
}
