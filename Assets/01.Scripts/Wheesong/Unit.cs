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
    protected EnemyHP enemyHP;
    protected Transform enemyTrs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHP = GetComponent<EnemyHP>();
    }

    private void OnEnable() // »ý¼º ½Ã
    {
        AgentData enemyData = Resources.Load<AgentData>($"EnemySO/{unitSOName}");
        enemyData = new AgentData(out enemyHP.hp, out attack, out speed, out attackDelay, out range, out attackRange);

        state = State.IDLE;
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

        if (DetectionRange(range))
            state = State.CHASE;
    }

    protected void ChaseNode()
    {
        Chase();

        if (!DetectionRange(range))
        {
            state = State.IDLE;
        }
        else if (DetectionLength(attackRange))
            state = State.ATTACK;
    }

    protected void AttackNode()
    {
        Attack();

        if (!DetectionLength(attackRange))
            state = State.CHASE;
    }

    protected bool DetectionRange(float range) //idle <=> chase
    {
        Collider2D[] getRange = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        if (getRange.Length > 0)
        {
            enemyTrs = getRange[0].transform;

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

    protected abstract void Idle();
    protected abstract void Chase();
    protected abstract void Attack();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
