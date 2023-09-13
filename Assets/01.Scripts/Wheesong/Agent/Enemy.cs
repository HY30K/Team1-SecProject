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
    protected SpriteRenderer sp;
    protected Collider2D col;
    protected Animator anim;
    protected Living livingHp;
    protected Transform unitTrs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        livingHp = GetComponent<Living>();
    }

    protected virtual void OnEnable()//생성 or Pop시에 능력치 초기화
    {
        DataSetting();
        col.enabled = true;
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
        if(unitTrs != null)
            sp.flipX = unitTrs.position.x - transform.position.x > 0 ? false : true;
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

    protected void DieNode()
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
        if (unitTrs.GetComponent<Unit>().state == State.DIE)
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
