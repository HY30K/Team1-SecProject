using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

public class WaveSystem : MonoSingleton<WaveSystem>
{
    [Header("Spawn")]
    [SerializeField] private List<SpawnData> spawnDatas = new List<SpawnData>();
    [SerializeField] private Transform[] spawnTrs;
    [SerializeField] private float spawnTime;

    [Header("UI")]
    [SerializeField] private DevilHp devilHp;
    [SerializeField] private Button nextBtn;
    [SerializeField] private TextMeshProUGUI turnningText;

    [Header("GameoverPanel")]
    [SerializeField] private Image niggerPanel;
    [SerializeField] private GameObject btns;

    [Header("Obj")]
    [SerializeField] private Transform unitParent;

    [HideInInspector] public int nowWave { get; private set; }
    [HideInInspector] public bool isWaving { get; private set; }

    private int waveEnemyCnt;
    private int waveUnitCnt;
    private bool isSpawning;

    const float costUpgradeGrape = 1.2f;
    const float hpUpgradeGrape = 1.15f;
    const float attackUpgradeGrape = 1.1f;

    private void Awake()
    {
        nextBtn.onClick.AddListener(NextWave);
    }

    public void NextWave()
    {
        if (isWaving || unitParent.childCount <= 0) return;

        waveUnitCnt = unitParent.childCount;
        isWaving = isSpawning = true;
        turnningText.text = $"WAVE {nowWave}";

        turnningText.transform.DOMoveX(960, 0.5f).SetEase(Ease.OutCubic).OnComplete(() => {
            turnningText.transform.DOMoveX(2500, 0.5f).SetEase(Ease.InCubic);
        });

        nextBtn.transform.DOMoveX(2200, 1f).SetEase(Ease.OutCubic).OnComplete(() =>{
            MoneyManager.Instance.UpdateEnemysCost(costUpgradeGrape);
            StartCoroutine(ParallelSpawnEnemy());
        });
    }

    private void EnemyDataUpgrade()
    {
        foreach (AgentData enemyData in AgentDictionary.Instance.enemyDatas.Values)
        {
            enemyData.hp *= hpUpgradeGrape;
            enemyData.attack *= attackUpgradeGrape;
        }
    }

    public void DieEnemy()
    {
        waveEnemyCnt--;
        if (waveEnemyCnt <= 0 && !isSpawning && isWaving)
        {
            WaveEnd();
            PlayerWin();
            isWaving = false;
        }
    }

    public void DieUnit()
    {
        waveUnitCnt--;
        if (waveUnitCnt <= 0 && isWaving)
        {
            StopCoroutine(ParallelSpawnEnemy());
            WaveEnd();
            PlayerLose();
            isWaving = false;
        }
    }

    public void WaveEnd()//공통적으로 웨이브 끝나고 할 거
    {
        nowWave++;

        turnningText.transform.DOMoveX(960, 0.5f).SetEase(Ease.OutCubic).OnComplete(() => {
            turnningText.transform.DOMoveX(2500, 0.5f).SetEase(Ease.InCubic);
        });

        nextBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $" Wave {nowWave}";
        nextBtn.transform.DOMoveX(1460, 1f).SetEase(Ease.OutCubic);
    }

    private void PlayerWin()//할거 없는거 같은데...
    {
        turnningText.text = "Wave Win!";
    }

    private void PlayerLose()
    {    
        turnningText.text = "Wave Lose!";

        //남아있는 적들 pop해주기
        EnemyHP[] enemies = FindObjectsOfType<EnemyHP>();
        foreach (EnemyHP dummyenemy in enemies)
        {
            dummyenemy.Die();
        }
        //마왕체력 깎기
        devilHp.OnHit(20);
    }

    public void GameOverSystem()
    {
        turnningText.text = "Game Over";

        turnningText.transform.DOMoveX(960, 0.5f).SetEase(Ease.OutCubic).OnComplete(() => {
            niggerPanel.gameObject.SetActive(true);
            niggerPanel.DOFade(1, 0.5f).OnComplete(() => { btns.SetActive(true); });
        });
    }

    private IEnumerator SequentiallySpawnEnemy() //위에있는 순부터 차례대로 생성
    {
        int wave = nowWave;
        if (nowWave >= spawnDatas.Count)//최대 생성
        {
            wave = spawnDatas.Count - 1;
            EnemyDataUpgrade();
        }

        for (int i = 0; i < spawnDatas[wave].enemyTuples.Count; i++)
        {
            string popName = spawnDatas[wave].enemyTuples[i].obj.name;
            for (int j = 0; j < spawnDatas[wave].enemyTuples[i].cnt; j++)
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

    private IEnumerator ParallelSpawnEnemy() //모든 몬스터가 랜덤순으로 생성
    {
        int wave = nowWave;
        if (nowWave >= spawnDatas.Count)//최대 생성, 이젠 몬스터 능력치를 강화시킨다.
        {
            wave = spawnDatas.Count - 1;
            EnemyDataUpgrade();
        }

        List<GameObject> randomEnemy = new List<GameObject>();
        for (int i = 0; i < spawnDatas[wave].enemyTuples.Count; i++)
            randomEnemy.Add(spawnDatas[wave].enemyTuples[i].obj);

        while (randomEnemy.Count > 0)
        {
            waveEnemyCnt++;

            int randomIndex = UnityEngine.Random.Range(0, randomEnemy.Count);
            string popName = spawnDatas[wave].enemyTuples[randomIndex].obj.name;
            Vector2 spawnPos = spawnTrs[UnityEngine.Random.Range(0, spawnTrs.Length)].position;
            PoolingManager.Instance.Pop(popName, spawnPos);

            spawnDatas[wave].enemyTuples[randomIndex].cnt--;
            if (spawnDatas[wave].enemyTuples[randomIndex].cnt <= 0)
            {
                randomEnemy.RemoveAt(randomIndex);
            }

            yield return new WaitForSeconds(spawnTime);
        }

        isSpawning = false;
        yield return null;
    }
}
