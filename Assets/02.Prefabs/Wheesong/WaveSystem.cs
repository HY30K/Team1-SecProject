using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTuple<T1, T2>
{  
    public T1 obj;
    public T2 cnt;
}

[System.Serializable]
public class SpawnData
{
    public List<EnemyTuple<GameObject, int>> enemyTuples;
}

public class WaveSystem : MonoBehaviour
{
    private static WaveSystem instance;
    public static WaveSystem Instance { get { return instance; } }

    [SerializeField] private List<SpawnData> spawnDatas = new List<SpawnData>();
    [SerializeField] private Transform spawnTrs;
    [SerializeField] private float spawnTime;

    [HideInInspector] public int nowWave { get; private set; }
    [HideInInspector] public bool isWaving { get; private set; }

    private int waveEnemyCnt;
    private bool isSpawning;

    const float hpUpgradeGrape = 1.15f;
    const float attackUpgradeGrape = 1.1f;

    public void NextWave()
    {
        isWaving = isSpawning = true;
        EnemyDataUpgrade();
        StartCoroutine(ParallelSpawnEnemy());
    }

    private void EnemyDataUpgrade()
    {
        AgentData[] enemyDatas = Resources.LoadAll<AgentData>("EnemySO");
        foreach (AgentData enemyData in enemyDatas)
        {
            enemyData.hp *= hpUpgradeGrape;
            enemyData.attack *= attackUpgradeGrape;
        }
    }

    public void DieEnemy()
    {
        waveEnemyCnt--;
        if(waveEnemyCnt <= 0 && !isSpawning)
            isWaving = false;
    }

    private IEnumerator SequentiallySpawnEnemy() //위에있는 순부터 차례대로 생성
    {
        for (int i = 0; i < spawnDatas[nowWave].enemyTuples.Count; i++)
        {
            string popName = spawnDatas[nowWave].enemyTuples[i].obj.name;
            for (int j = 0; j < spawnDatas[nowWave].enemyTuples[i].cnt; j++)
            {
                waveEnemyCnt++;

                PoolingManager.instance.Pop(popName, spawnTrs.position);
                yield return new WaitForSeconds(spawnTime);
            }
        }

        isSpawning = false;
        yield return null;
    }

    private IEnumerator ParallelSpawnEnemy() //모든 몬스터가 랜덤순으로 생성
    {
        List<GameObject> randomEnemy = new List<GameObject>();
        for (int i = 0; i < spawnDatas[nowWave].enemyTuples.Count; i++)
            randomEnemy.Add(spawnDatas[nowWave].enemyTuples[i].obj);

        while (randomEnemy.Count > 0)
        {
            waveEnemyCnt++;

            int randomIndex = UnityEngine.Random.Range(0, randomEnemy.Count);
            string popName = spawnDatas[nowWave].enemyTuples[randomIndex].obj.name;
            PoolingManager.instance.Pop(popName, spawnTrs.position);

            spawnDatas[nowWave].enemyTuples[randomIndex].cnt--;
            if (spawnDatas[nowWave].enemyTuples[randomIndex].cnt <= 0)
            {
                randomEnemy.Remove(spawnDatas[nowWave].enemyTuples[randomIndex].obj);
            }

            yield return new WaitForSeconds(spawnTime);
        }

        isSpawning = false;
        yield return null;
    }
}
