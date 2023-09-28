using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int money { get; private set; }

    [SerializeField] private int startMoney;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Transform unitCase;

    private Dictionary<string, ValueTuple<int, int>> unitCostDictionary = new Dictionary<string, ValueTuple<int, int>>();//���� �̸�, ��/�ε���
    private Dictionary<string, int> enemyCostDictionary = new Dictionary<string, int>();//�� �̸�, ��
    private List<TextMeshProUGUI> unitCostText = new List<TextMeshProUGUI>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        CostSet();
    }

    private void CostSet()
    {
        money = startMoney;

        //���� �ڽ�Ʈ �ؽ�Ʈ ��������
        for (int i = 0; i < unitCase.childCount; i++)
        {
            unitCostText.Add(unitCase.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>());
        }
        moneyText.text = $"Money : {money}";

        //����/�� �ڽ�Ʈ �ʱ�ȭ
        AgentData[] units = Resources.LoadAll<AgentData>("UnitSO");
        AgentData[] enemys = Resources.LoadAll<AgentData>("EnemySO");

        for (int i = 0; i < units.Length; i++)
            unitCostDictionary.Add(units[i].name, (units[i].cost, -1));

        for (int i = 0; i < enemys.Length; i++)
            enemyCostDictionary.Add(enemys[i].name, enemys[i].cost);

        for (int i = 0; i < 5; i++)
        {
            string name = unitCase.GetChild(i).name.Replace("_Image", "");
            var dict = unitCostDictionary[name];
            var newValue = new ValueTuple<int, int>(dict.Item1, i);

            unitCostText[i].text = dict.Item1.ToString();
            unitCostDictionary[name] = newValue;
        }
    }

    public void UpdateUnitCost(string unitName, int value)
    {
        var dict = unitCostDictionary[unitName];
        var newValue = new ValueTuple<int, int>(dict.Item1 + value, dict.Item2);
        unitCostDictionary[unitName] = newValue;

        int index = dict.Item2;
        unitCostText[index].text = unitCostDictionary[unitName].Item1.ToString();
    }

    public void UpdateEnemysCost(float value)
    {
        foreach(var key in enemyCostDictionary.Keys.ToList())
            enemyCostDictionary[key] = Mathf.CeilToInt(enemyCostDictionary[key] * value);
    }

    public int UnitCost(string unitName)
    {
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
