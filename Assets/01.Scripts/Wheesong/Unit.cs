using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { IDLE = 0, CHASE = 1, ATTACK = 2, DIE = 3, NULL = 4}

public class Unit : MonoBehaviour
{
    [Header("SOName")]
    [SerializeField] private string unitSOName;

    protected float hp;
    protected float speed;
    protected float attack;
    protected float attackDelay;
    protected float range;
    protected float attackRange;

    private void OnEnable() // »ý¼º ½Ã
    {
        AgentData enemyData = Resources.Load<AgentData>($"EnemySO/{unitSOName}");
        enemyData = new AgentData(out hp, out attack, out speed, out attackDelay, out range, out attackRange);

        IdleNode();
    }

    private void UnitBrain()
    {
        //switch()
    }

    protected virtual void IdleNode()
    {
        if (DetectionRange(range))
            ChaseNode();
    }

    protected virtual void ChaseNode()
    {
        if (DetectionRange(attackRange))
            ChaseNode();
        else if (!DetectionRange(range))
            IdleNode();
    }

    protected virtual void AttackNode()
    {
        if(!DetectionRange(attackRange))
            AttackNode();
    }

    protected bool DetectionRange(float range)
    {
        bool getRange = Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Enemy"));
        return getRange;
    }

    protected virtual void Die()
    {
        PoolingManager.instance.Push(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
