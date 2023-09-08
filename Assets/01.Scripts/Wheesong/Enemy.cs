using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("SOName")]
    [SerializeField] private string enemySOName;

    protected float hp;
    protected float speed;
    protected float attack;
    protected float attackDelay;
    protected float range;
    protected float attackRange;

    protected virtual void OnEnable()//���� or Pop�ÿ� �ɷ�ġ �ʱ�ȭ
    {
        DataSetting();
    }

    void DataSetting()
    {
        AgentData enemyData = Resources.Load<AgentData>($"EnemySO/{enemySOName}");
        hp = enemyData.hp;
        speed = enemyData.speed;
        attack = enemyData.attack;
        attackDelay = enemyData.attackDelay;
        range = enemyData.range;
        attackRange = enemyData.attackRange;
    }
}
