using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Dictionary<string, AgentData> enemyDatas = new Dictionary<string, AgentData>();

    [Header("Spawn")]
    [SerializeField] private List<SpawnData> spawnDatas = new List<SpawnData>();
    [SerializeField] private Transform[] spawnTrs;
    [SerializeField] private float spawnTime;

    [Header("Other")]
    [SerializeField] private DevilHp devilHp;
    [SerializeField] private Transform unitParent;
    [SerializeField] private Button nextBtn;

    [HideInInspector] public int nowWave { get; private set; }
    [HideInInspector] public bool isWaving { get; private set; }

    private int waveEnemyCnt;
    private int waveUnitCnt;
    private bool isSpawning;

    const float hpUpgradeGrape = 1.15f;
    const float attackUpgradeGrape = 1.1f;

    private void Awake()
    {
        if(instance == null) instance = this;

        foreach (AgentData enemyData in Resources.LoadAll<AgentData>("EnemySO"))
        {
            enemyDatas.Add(enemyData.name, Instantiate(enemyData));
        }

        nextBtn.onClick.AddListener(NextWave);
    }

    public void NextWave()
    {
        if (isWaving) return;

        isWaving = isSpawning = true;
        waveUnitCnt = unitParent.childCount;

        EnemyDataUpgrade();
        StartCoroutine(ParallelSpawnEnemy());
    }

    private void EnemyDataUpgrade()
    {
        foreach (AgentData enemyData in enemyDatas.Values)
        {
            enemyData.hp *= hpUpgradeGrape;
            enemyData.attack *= attackUpgradeGrape;
        }
    }

    public void DieEnemy()
    {
        waveEnemyCnt--;
        if (waveEnemyCnt <= 0 && !isSpawning)
        {
            PlayerWin();
            isWaving = false;
        }
    }

    public void DieUnit()
    {
        waveUnitCnt--;
        if (waveUnitCnt <= 0)
        {
            StopAllCoroutines();
            PlayerLose();
            isWaving = false;
        }
    }

    private void PlayerWin()//�Ұ� ���°� ������...
    {
        nowWave++;
    }

    private void PlayerLose()
    {
        nowWave++;

        //�����ִ� ���� pop���ֱ�
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy dummyenemy in enemies)
        {
            PoolingManager.Instance.Push(dummyenemy.gameObject);
        }
        //����ü�� ���
        devilHp.OnHit(20);
    }

    private IEnumerator SequentiallySpawnEnemy() //�����ִ� ������ ���ʴ�� ����
    {
        for (int i = 0; i < spawnDatas[nowWave].enemyTuples.Count; i++)
        {
            string popName = spawnDatas[nowWave].enemyTuples[i].obj.name;
            for (int j = 0; j < spawnDatas[nowWave].enemyTuples[i].cnt; j++)
            {
                waveEnemyCnt++;

                Vector2 spawnPos = spawnTrs[UnityEngine.Random.Range(0, spawnDatas.Count)].position;
                PoolingManager.Instance.Pop(popName, spawnPos);
                yield return new WaitForSeconds(spawnTime);
            }
        }

        isSpawning = false;
        yield return null;
    }

    private IEnumerator ParallelSpawnEnemy() //��� ���Ͱ� ���������� ����
    {
        List<GameObject> randomEnemy = new List<GameObject>();
        for (int i = 0; i < spawnDatas[nowWave].enemyTuples.Count; i++)
            randomEnemy.Add(spawnDatas[nowWave].enemyTuples[i].obj);

        while (randomEnemy.Count > 0)
        {
            waveEnemyCnt++;

            int randomIndex = UnityEngine.Random.Range(0, randomEnemy.Count);
            string popName = spawnDatas[nowWave].enemyTuples[randomIndex].obj.name;
            Vector2 spawnPos = spawnTrs[UnityEngine.Random.Range(0, spawnDatas.Count)].position;
            PoolingManager.Instance.Pop(popName, spawnPos);

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
