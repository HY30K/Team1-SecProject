using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : Enemy
{
    bool canAttack = true;

    protected override void Idle()
    {
        
    }

    protected override void Chase()
    {
        Vector2 dir = unitTrs.position - transform.position;
        rb.velocity = dir * speed;
    }

    protected override void Attack()
    {
        if (!canAttack) return;

        unitTrs.GetComponent<UnitHp>().OnHit(attack);
        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
