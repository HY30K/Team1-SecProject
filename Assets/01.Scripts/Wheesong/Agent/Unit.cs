using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Agent
{
    [Header("SOName")]
    [SerializeField] private string unitSOName;

    protected Transform enemyTrs;

    private void Update()
    {
        Brain();
        sp.material.SetTexture("Sprite", sp.sprite.texture);
        if (enemyTrs != null)
            sp.flipX = enemyTrs.position.x - transform.position.x > 0 ? false : true;
    }

    protected override void DataSetting()
    {
        AgentData unitData = Resources.Load<AgentData>($"UnitSO/{unitSOName}");
        livingHp.HpSetting(unitData.hp);
        speed = unitData.Speed;
        attack = unitData.attack;
        attackDelay = unitData.AttackDelay;
        range = unitData.Range;
        attackRange = unitData.AttackRange;
    }

    protected override void IdleNode()
    {
        Idle();

        if (DetectionRange(range))
        {
            isChangeState = true;
            state = State.CHASE;
        }
    }

    protected override void ChaseNode()
    {
        Chase();

        if (!DetectionRange(range))
        {
            isChangeState = true;
            rb.velocity = Vector2.zero;
            state = State.IDLE;
        }
        else if (DetectionLength(attackRange))
        {
            isChangeState = true;
            rb.velocity = Vector2.zero;
            state = State.ATTACK;
        }
    }

    protected override void AttackNode()
    {
        Attack();

        if (!DetectionLength(attackRange) ||
            enemyTrs.GetComponent<Enemy>().state == State.DIE) //¶§¸®´ø ¾ê°¡ Á×¾úÀ»¶§
        {
            isChangeState = true;
            state = State.CHASE;
        }
    }

    protected override void DieNode()
    {
        Die();
    }

    protected bool DetectionRange(float range) //idle <=> chase
    {
        Collider2D[] getRange = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        if (getRange.Length > 0)
        {
            float length = 99;
            foreach (Collider2D enemys in getRange)
            {
                if (Vector2.Distance(enemys.transform.position, transform.position) < length && enemys.enabled)
                {
                    length = Vector2.Distance(enemys.transform.position, transform.position);
                    enemyTrs = enemys.transform;
                }
            }
            if (length == 99)
                return false;
            return true;
        }
        enemyTrs = null;
        return false;
    }

    protected bool DetectionLength(float range) //chase <=> attack
    {
        float length = Vector2.Distance(enemyTrs.position, transform.position);
        if (enemyTrs.GetComponent<Enemy>().state == State.DIE)
            return false;
        return length <= range ? true : false; 
    }

    protected abstract void Idle();
    protected abstract void Chase();
    protected abstract void Attack();
    protected abstract void Die();
}
