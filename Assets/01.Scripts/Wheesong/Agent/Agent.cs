using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { IDLE = 0, CHASE = 1, ATTACK = 2, DIE = 3, NULL = 4 }

public abstract class Agent : MonoBehaviour
{
    public State state;

    public float speed;
    public float attack;
    public float attackDelay;
    public float range;
    public float attackRange;

    protected Rigidbody2D rb;
    protected SpriteRenderer sp;
    protected Collider2D col;
    protected Animator anim;
    protected Living livingHp;

    public bool isChangeState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        livingHp = GetComponent<Living>();
    }

    protected virtual void OnEnable() // »ý¼º ½Ã
    {
        DataSetting();
        col.enabled = true;
        state = State.IDLE;
    }

    protected abstract void DataSetting();

    protected void Brain()
    {
        switch (state)
        {
            case State.IDLE:
                ChangeState("Idle");
                IdleNode();
                break;
            case State.CHASE:
                ChangeState("Chase");
                ChaseNode();
                break;
            case State.ATTACK:
                ChangeState("Attack");
                AttackNode();
                break;
            case State.DIE:
                ChangeState("Die");
                DieNode();
                break;
        }
    }

    protected void ChangeState(string triggerName)
    {
        if (!isChangeState) return;
        isChangeState = false;
        anim.SetTrigger(triggerName);
    }

    protected abstract void IdleNode();
    protected abstract void ChaseNode();
    protected abstract void AttackNode();
    protected abstract void DieNode();

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}
