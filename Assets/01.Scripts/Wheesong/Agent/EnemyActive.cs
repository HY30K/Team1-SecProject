using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : Enemy
{
    bool canAttack = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        canAttack = true;
    }

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

        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    private void OnDisable()
    {
        canAttack = false;
        StopAllCoroutines();
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        unitTrs.GetComponent<UnitHp>().OnHit(attack);
        canAttack = true;
    }
}
