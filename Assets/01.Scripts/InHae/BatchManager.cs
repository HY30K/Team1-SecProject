using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class BatchManager : MonoBehaviour
{
    [Header("UnitData")]
    [SerializeField] private Transform unitParent;
    [SerializeField] private GameObject[] units;
    [SerializeField] private GameObject[] unitAlphas;
    [SerializeField] private GameObject[] unitImage;
    [SerializeField] private int[] unitsCost;

    private Dictionary<string, GameObject> unitsDictionary = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> unitAlphasDictionary = new Dictionary<string, GameObject>();
    private Dictionary<string, int> unitCostDictionary = new Dictionary<string, int>();
    private GameObject currentUnitAlpha;
    List<TextMeshProUGUI> costText = new List<TextMeshProUGUI>();

    static public BatchManager Instance;

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < units.Length; i++)
        {
            unitsDictionary.Add(units[i].name, units[i]);
            unitAlphasDictionary.Add(unitAlphas[i].name, unitAlphas[i]);
            unitCostDictionary.Add(unitImage[i].name, unitsCost[i]);
            costText.Add(unitImage[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            costText[i].text = unitsCost[i].ToString();
        }
    }

    public void UnitCreate(Vector2 batchPos, string unitAlphaName)
    {
        currentUnitAlpha = PoolingManager.Instance.Pop(unitAlphasDictionary[unitAlphaName].name, batchPos);
    }

    public void UnitDestroy()
    {
        PoolingManager.Instance.Push(currentUnitAlpha);
    }

    public void UnitAlphaBatch(Vector2 batchPos)
    {
        currentUnitAlpha.transform.position = BatchTile.Instance.Vector2IntPos(batchPos);
    }

    public void UnitBatch(Vector2 batchPos, string unitName)
    {
        int cost = unitCostDictionary[unitName];
        if (WaveSystem.Instance.isWaving
            || !MoneyManager.Instance.EnoughMoney(cost)) return;
        MoneyManager.Instance.ChangeMoney(-cost);

        if (BatchCheck.batchble && BatchTile.Instance.IsBatchAble(batchPos))
        {
            currentUnitAlpha.transform.Find("BatchArea").gameObject.SetActive(false);
            PoolingManager.Instance.Pop(unitsDictionary[unitName].name, BatchTile.Instance.Vector2IntPos(batchPos), unitParent);
        }
        PoolingManager.Instance.Push(currentUnitAlpha);
    }

    public void UnitCostUpgrade(string unitImageName, int value)
    {
        int index = Array.IndexOf(unitCostDictionary.Keys.ToArray(), unitImageName);
        unitsCost[index] += value;

        for (int i = 0; i < unitsCost.Length; i++)
        {
            costText[i].text = unitsCost[i].ToString();
        }
    }
}

