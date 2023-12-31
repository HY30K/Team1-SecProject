using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BatchManager : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField] private GameObject lightingEffect;

    [Header("UnitData")]
    [SerializeField] private Transform unitParent;
    [SerializeField] private GameObject[] units;
    [SerializeField] private GameObject[] unitAlphas;
    //[SerializeField] private GameObject[] unitImage;

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
        string alphaName = unitAlphaName.Replace("_Image", ""); 
        currentUnitAlpha = PoolingManager.Instance.Pop(unitAlphasDictionary[alphaName].name, batchPos);
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
        int cost = MoneyManager.Instance.UnitCost(unitName);

        if (WaveSystem.Instance.isWaving
            || !MoneyManager.Instance.EnoughMoney(cost)) return;

        MoneyManager.Instance.UpdateMoney(-cost);

        if (BatchCheck.batchble && BatchTile.Instance.IsBatchAble(batchPos))
        {
            currentUnitAlpha.transform.Find("BatchArea").gameObject.SetActive(false);

            Vector2 installPos = BatchTile.Instance.Vector2IntPos(batchPos);
            PoolingManager.Instance.Pop(unitsDictionary[unitName].name, installPos, unitParent);

            GameObject effect = PoolingManager.Instance.Pop(lightingEffect.name, installPos + new Vector2(0, 1f));
            StartCoroutine(PushEffect(effect));
        }
        PoolingManager.Instance.Push(currentUnitAlpha);
    }

    IEnumerator PushEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1f);
        PoolingManager.Instance.Push(effect);
    }
}

