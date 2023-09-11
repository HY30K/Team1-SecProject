using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public State state;

    [Header("SOName")]
    [SerializeField] private string enemySOName;

    protected float speed;
    protected float attack;
    protected float attackDelay;
    protected float range;
    protected float attackRange;

    protected Rigidbody2D rb;
    protected Living livingHp;
    protected Transform unitTrs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        livingHp = GetComponent<Living>();
    }

    protected virtual void OnEnable()//생성 or Pop시에 능력치 초기화
    {
        DataSetting();
        state = State.IDLE;
    }

    void DataSetting()
    {
        AgentData enemyData = WaveSystem.Instance.enemyDatas[enemySOName];
        livingHp.HpSetting(enemyData.hp);
        speed = enemyData.Speed;
        attack = enemyData.attack;
        attackDelay = enemyData.AttackDelay;
        range = enemyData.Range;
        attackRange = enemyData.AttackRange;
    }

    private void Update()
    {
        UnitBrain();
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

        if (FindUnit())
        {
            state = State.CHASE;
        }
    }

    protected void ChaseNode()
    {
        Chase();

        if (!FindUnit())
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
            !unitTrs.gameObject.activeSelf) //때리던 얘가 죽었을때
            state = State.CHASE;
    }

    private bool FindUnit()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        if (units.Length > 0)
        {
            float length = 99;
            foreach (Unit unit in units)
            {
                if (Vector2.Distance(unit.transform.position, transform.position) < length)
                {
                    length = Vector2.Distance(unit.transform.position, transform.position);
                    unitTrs = unit.transform;
                }
            }
            return true;
        }
        unitTrs = null;
        return false;
    }

    protected bool DetectionLength(float range) //chase <=> attack
    {
        float length = Vector2.Distance(unitTrs.position, transform.position);
        return length <= range ? true : false;
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
