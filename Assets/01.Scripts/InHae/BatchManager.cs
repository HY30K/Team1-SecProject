using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BatchManager : MonoBehaviour
{
    [SerializeField] private GameObject[] units;
    [SerializeField] private GameObject[] unitAlphas;
    private Dictionary<string, GameObject> unitsDictionary = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> unitAlphasDictionary = new Dictionary<string, GameObject>();
    private GameObject currentUnitAlpha;

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
        }
    }

    public void UnitCreate(Vector2 batchPos, string unitAlphaName)
    {
        currentUnitAlpha = Instantiate(unitAlphasDictionary[unitAlphaName], batchPos, quaternion.identity);
    }

    public void UnitAlphaBatch(Vector2 batchPos)
    {
        currentUnitAlpha.transform.position = batchPos;
    }

    public void UnitBatch(Vector2 batchPos, string unitName)
    {
        if (BatchCheck.batchble && BatchTile.Instance.IsBatchAble(batchPos))
        {
            currentUnitAlpha.transform.Find("BatchArea").gameObject.SetActive(false);
            Instantiate(unitsDictionary[unitName], BatchTile.Instance.Vector2IntPos(batchPos), quaternion.identity);
            Destroy(currentUnitAlpha);
        }
        else
            Destroy(currentUnitAlpha);
        }
}
