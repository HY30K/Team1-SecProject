using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    protected override void Die()//죽을때 해줄거(아직 없음)
    {
        col.enabled = false;
    }

    private void OnDisable()
    {
        canAttack = false;
        StopAllCoroutines();
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        if (unitTrs != null)
        {
            unitTrs.GetComponent<UnitHp>().OnHit(attack);
        }
        canAttack = true;
    }
}
