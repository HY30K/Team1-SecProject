using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int money { get; private set; }

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Transform unitCase;

    private Dictionary<string, ValueTuple<int, int>> unitCostDictionary = new Dictionary<string, ValueTuple<int, int>>();//유닛 이름, 돈/인덱스
    private Dictionary<string, int> enemyCostDictionary = new Dictionary<string, int>();//유닛 이름, 돈
    private List<TextMeshProUGUI> unitCostText = new List<TextMeshProUGUI>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        //유닛 코스트 텍스트 가져오기
        for (int i = 0; i < unitCase.childCount; i++)
        {
            unitCostText.Add(unitCase.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>());
        }

        //유닛/적 코스트 초기화
        AgentData[] units = Resources.LoadAll<AgentData>("UnitSO");
        AgentData[] enemys = Resources.LoadAll<AgentData>("EnemyS0");

        for (int i = 0; i < units.Length; i++)
        {
            Debug.Log(units[i].name);
            unitCostDictionary.Add(units[i].name, ( units[i].cost, i));
            unitCostText[i].text = units[i].cost.ToString();
        }
        for (int i = 0; i < enemys.Length; i++)
        {
            enemyCostDictionary.Add(enemys[i].name, enemys[i].cost);
        }
    }

    public void UpdateUnitCost(string unitName, int value)
    {
        var dict = unitCostDictionary[unitName];
        var newValue = new ValueTuple<int, int>(dict.Item1 + value, dict.Item2);
        unitCostDictionary[unitName] = newValue;

        int index = dict.Item2;
        unitCostText[index].text = unitCostDictionary[unitName].ToString();
    }

    public void UpdateEnemysCost(float value)
    {
        foreach(var key in enemyCostDictionary.Keys.ToList())
            enemyCostDictionary[key] = Mathf.CeilToInt(enemyCostDictionary[key] * value);
    }

    public int UnitCost(string unitName)
    {
        Debug.Log(unitName);
        return unitCostDictionary[unitName].Item1;
    }

    public int EnemyCost(string enemyName)
    {
        return enemyCostDictionary[enemyName];
    }

    public void UpdateMoney(int value)
    {
        money += value;
        moneyText.text = $"Money : {money}";
    }

    public bool EnoughMoney(int value)
    {
        return money - value >= 0 ? true : false;
    }
}
