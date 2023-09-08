using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Unit/AgentData")]
public class AgentData : ScriptableObject
{
    public float hp;
    public float attack;
    public float speed;
    public float attackDelay;
    public float range;
    public float attackRange;
}
