using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("SOName")]
    [SerializeField] private string unitSOName;

    protected float hp;
    protected float speed;
    protected float attack;
    protected float attackDelay;
    protected float range;

    private void OnEnable() // »ý¼º ½Ã
    {
        AgentData enemyData = Resources.Load<AgentData>($"EnemySO/{unitSOName}");
        enemyData = new AgentData(out hp, out attack, out speed, out attackDelay, out range);
    }

    protected virtual void IdleNode()
    {

    }

    protected virtual void ChaseNode()
    {

    }

    protected virtual void AttackNode()
    {

    }

    protected bool EnemyRange()
    {
        return true;
    }

    protected virtual void Die()
    {
        PoolingManager.instance.Push(gameObject);
    }
}
