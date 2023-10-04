using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : Agent
{
    [Header("SOName")]
    [SerializeField] private string enemySOName;

    protected Transform unitTrs;

    private void Update()
    {
        Brain();
        sp.material.SetTexture("Sprite", sp.sprite.texture);
        if (unitTrs != null)
            sp.flipX = unitTrs.position.x - transform.position.x > 0 ? false : true;
    }

    protected override void DataSetting()
    {
        AgentData enemyData = AgentDictionary.Instance.enemyDatas[enemySOName];
        livingHp.HpSetting(enemyData.hp);
        speed = enemyData.Speed;
        attack = enemyData.attack;
        attackDelay = enemyData.AttackDelay;
        range = enemyData.Range;
        attackRange = enemyData.AttackRange;
    }

    protected override void IdleNode()
    {
        Idle();

        if (FindUnit())
        {
            isChangeState = true;
            state = State.CHASE;
        }
    }

    protected override void ChaseNode()
    {
        Chase();

        if (!FindUnit())
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
            unitTrs.GetComponent<Unit>().state == State.DIE) //¶§¸®´ø ¾ê°¡ Á×¾úÀ»¶§
        {
            isChangeState = true;
            state = State.CHASE;
        }
    }

    protected override void DieNode()
    {
        Die();
    }

    private bool FindUnit()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        if (units.Length > 0)
        {
            float length = 99;
            foreach (Unit unit in units)
            {
                if (Vector2.Distance(unit.transform.position, transform.position) < length && unit.state != State.DIE)
                {
                    length = Vector2.Distance(unit.transform.position, transform.position);
                    unitTrs = unit.transform;
                }
            }
            if (length == 99) 
                return false;
            return true;
        }
        unitTrs = null;
        return false;
    }

    protected bool DetectionLength(float range) //chase <=> attack
    {
        float length = Vector2.Distance(unitTrs.position, transform.position);
        if (unitTrs.GetComponent<Unit>().state == State.DIE)
            return false;
        return length <= range ? true : false;
    }

    protected abstract void Idle();
    protected abstract void Chase();
    protected abstract void Attack();
    protected abstract void Die();
}
