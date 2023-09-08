using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UnitActive : Unit
{
    bool canAttack = true;

    protected override void Idle()//������ �ֱ�
    {
        
    }

    protected override void Chase()//�� �ѱ�
    {
        Vector2 dir = enemyTrs.position - transform.position;
        rb.velocity = dir * speed;
    }
    
    protected override void Attack()//����
    {
        if (!canAttack) return;
        UnityEngine.Debug.Log("����1");
        enemyTrs.GetComponent<EnemyHP>().OnHit(attack);
        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
