using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : Living
{
    [SerializeField] float dropOdds;

    private Enemy enemy;

    public override void Die()
    {
        base.Die();
        enemy = GetComponent<Enemy>();
        enemy.state = State.DIE;
        enemy.isChangeState = true;

        DropItem();

        StartCoroutine(PushEnemy(1));
    }

    private void DropItem()
    {
        int odds = Random.Range(0, 101);
        if (odds < dropOdds)
        {
            Item[] items = Resources.LoadAll<Item>("Item");
            int itemIndex = Random.Range(0, items.Length);
            PoolingManager.Instance.Pop(items[itemIndex].name, transform.position);
        }
    }

    IEnumerator PushEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        WaveSystem.Instance.DieEnemy();
        PoolingManager.Instance.Push(gameObject);
    }
}
