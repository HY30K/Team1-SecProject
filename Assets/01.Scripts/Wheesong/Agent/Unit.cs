using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Agent
{
    [Header("SOName")]
    [SerializeField] private string unitSOName;

    public int level;

    protected Transform enemyTrs;

    private void Update()
    {
        Brain();
        sp.material.SetTexture("Sprite", sp.sprite.texture);
        if (enemyTrs != null)
            sp.flipX = enemyTrs.position.x - transform.position.x > 0 ? false : true;
    }

    private void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} ����");
    }

    protected override void DataSetting()
    {
        AgentData unitData = AgentDictionary.Instance.unitDatas[unitSOName];
        livingHp.HpSetting(unitData.hp);
        level = unitData.level;
        speed = unitData.Speed;
        attack = unitData.attack;
        attackDelay = unitData.AttackDelay;
        range = unitData.Range;
        attackRange = unitData.AttackRange;
    }

    protected override void IdleNode()
    {
        Idle();

        if (FindEnemy())
        {
            isChangeState = true;
            state = State.CHASE;
        }
    }

    protected override void ChaseNode()
    {
        Chase();

        if (!FindEnemy())
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
            enemyTrs.GetComponent<Enemy>().state == State.DIE) //������ �갡 �׾�����
        {
            isChangeState = true;
            state = State.CHASE;
        }
    }

    protected override void DieNode()
    {
        Die();
    }

    private bool FindEnemy()
    {
        Enemy[] enemys = FindObjectsOfType<Enemy>();
        if (enemys.Length > 0)
        {
            float length = 99;
            foreach (Enemy enemy in enemys)
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) < length && enemy.state != State.DIE)
                {
                    length = Vector2.Distance(enemy.transform.position, transform.position);
                    enemyTrs = enemy.transform;
                }
            }
            if (length == 99)
                return false;
            return true;
        }
        enemyTrs = null;
        return false;
    }

    //protected bool DetectionRange(float range) //idle <=> chase
    //{
    //    Collider2D[] getRange = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
    //    if (getRange.Length > 0)
    //    {
    //        float length = 99;
    //        foreach (Collider2D enemys in getRange)
    //        {
    //            if (Vector2.Distance(enemys.transform.position, transform.position) < length && enemys.enabled)
    //            {
    //                length = Vector2.Distance(enemys.transform.position, transform.position);
    //                enemyTrs = enemys.transform;
    //            }
    //        }
    //        if (length == 99)
    //            return false;
    //        return true;
    //    }
    //    enemyTrs = null;
    //    return false;
    //}

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
