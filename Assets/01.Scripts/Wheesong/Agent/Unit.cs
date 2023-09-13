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
    protected SpriteRenderer sp;
    protected Collider2D col;
    protected Animator anim;
    protected Living livingHp;
    protected Transform enemyTrs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        livingHp = GetComponent<Living>();
    }

    private void OnEnable() // 생성 시
    {
        DataSetting();
        col.enabled = true;
        state = State.IDLE;
    }

    private void Update()
    {
        UnitBrain();

        if (enemyTrs != null)
            sp.flipX = enemyTrs.position.x - transform.position.x > 0 ? false : true;
    }

    void DataSetting()
    {
        AgentData unitData = Resources.Load<AgentData>($"UnitSO/{unitSOName}");
        livingHp.HpSetting(unitData.hp);
        speed = unitData.Speed;
        attack = unitData.attack;
        attackDelay = unitData.AttackDelay;
        range = unitData.Range;
        attackRange = unitData.AttackRange;
    }

    private void UnitBrain()
    {
        switch (state)
        {
            case State.IDLE:
                anim.SetTrigger("Idle");
                IdleNode();
                break;
            case State.CHASE:
                anim.SetTrigger("Chase");
                ChaseNode();
                break;
            case State.ATTACK:
                anim.SetTrigger("Attack");
                AttackNode();
                break;
            case State.DIE:
                anim.SetTrigger("Die");
                DieNode();
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

    protected void DieNode()
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
        if (enemyTrs.GetComponent<Enemy>().state == State.DIE)
            return false;
        return length <= range ? true : false; 
    }

    protected abstract void Idle();
    protected abstract void Chase();
    protected abstract void Attack();
    protected abstract void Die();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
