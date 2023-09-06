using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Unit/AgentData")]
public class AgentData : ScriptableObject
{
    public float hp;
    public float attack;
    public float speed { get; private set; }
    public float attackDelay { get; private set; }
    public float range { get; private set; }

    public AgentData(out float hp, out float attack, out float speed,
                    out float attackDelay, out float range)
    {
        hp = this.hp;
        attack = this.attack;
        speed = this.speed;
        attackDelay = this.attackDelay;
        range = this.range;
    }
}
