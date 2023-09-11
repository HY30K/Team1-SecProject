using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Unit/AgentData")]
public class AgentData : ScriptableObject
{
    public float hp;
    public float attack;
    [SerializeField] private float speed;
    [SerializeField] private float attackDelay;
    [SerializeField] private float range;
    [SerializeField] private float attackRange;

    public float Speed { get { return speed; } }
    public float AttackDelay { get { return attackDelay; } }
    public float Range { get { return range; } }
    public float AttackRange { get { return attackRange; } }
}
