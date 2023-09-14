using System.Collections;
using System.Collections.Generic;
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
        
        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    protected override void Die()//죽을때 해줄거(아직 없음)
    {
        
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
        yield return new WaitForSeconds(attackDelay);
        if(enemyTrs != null)
            enemyTrs.GetComponent<EnemyHP>().OnHit(attack);
        canAttack = true;
    }
}
