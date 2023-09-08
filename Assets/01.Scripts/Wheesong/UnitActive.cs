using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UnitActive : Unit
{
    bool canAttack = true;

    protected override void Idle()//가만히 있기
    {
        
    }

    protected override void Chase()//적 쫓기
    {
        Vector2 dir = enemyTrs.position - transform.position;
        rb.velocity = dir * speed;
    }
    
    protected override void Attack()//공격
    {
        if (!canAttack) return;
        UnityEngine.Debug.Log("공격1");
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
