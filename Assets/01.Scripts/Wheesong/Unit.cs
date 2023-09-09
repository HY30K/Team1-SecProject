using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { IDLE = 0, CHASE = 1, ATTACK = 2, DIE = 3, NULL = 4}

public abstract class Unit : MonoBehaviour
{
    public State state;

    [Header("SOName")]
    [SerializeField] private string unitSOName;

    protected float speed;
    protected float attack;
    protected float attackDelay;
    protected float range;
    protected float attackRange;

    protected Rigidbody2D rb;
    protected Living livingHp;
    protected Transform enemyTrs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        livingHp = GetComponent<Living>();
    }

    private void OnEnable() // 생성 시
    {
        DataSetting();
        state = State.IDLE;
    }

    private void Update()
    {
        UnitBrain();
    }

    void DataSetting()
    {
        AgentData unitData = Resources.Load<AgentData>($"UnitSO/{unitSOName}");
        livingHp.HpSetting(unitData.hp);
        speed = unitData.speed;
        attack = unitData.attack;
        attackDelay = unitData.attackDelay;
        range = unitData.range;
        attackRange = unitData.attackRange;
    }

    private void UnitBrain()
    {
        switch (state)
        {
            case State.IDLE:
                IdleNode();
                break;
            case State.CHASE:
                ChaseNode();
                break;
            case State.ATTACK:
                AttackNode();
                break;
        }
    }

    protected void IdleNode()
    {
        Idle();

        if (DetectionRange(range))
            state = State.CHASE;
    }

    protected void ChaseNode()
    {
        Chase();

        if (!DetectionRange(range))
        {
            rb.velocity = Vector2.zero;
            state = State.IDLE;
        }
        else if (DetectionLength(attackRange))
        {
            rb.velocity = Vector2.zero;
            state = State.ATTACK;
        }
    }

    protected void AttackNode()
    {
        Attack();

        if (!DetectionLength(attackRange) ||
            !enemyTrs.gameObject.activeSelf) //때리던 얘가 죽었을때
            state = State.CHASE;
    }

    protected bool DetectionRange(float range) //idle <=> chase
    {
        Collider2D[] getRange = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        if (getRange.Length > 0)
        {
            float length = 99;
            foreach (Collider2D enemys in getRange)
            {
                if (Vector2.Distance(enemys.transform.position, transform.position) < length)
                {
                    length = Vector2.Distance(enemys.transform.position, transform.position);
                    enemyTrs = enemys.transform;
                }
            }
            return true;
        }
        enemyTrs = null;
        return false;
    }

    protected bool DetectionLength(float range) //chase <=> attack
    {
        float length = Vector2.Distance(enemyTrs.position, transform.position);
        return length <= range ? true : false; 
    }

    public void HitUnit(Transform enemy)//용사한테 맞았을때
    {
        state = State.CHASE;
        enemyTrs = enemy;
    }

    protected abstract void Idle();
    protected abstract void Chase();
    protected abstract void Attack();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
