using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCost : MonoBehaviour
{
    public int enemyCost { get; private set; }

    public void Start()
    {
        enemyCost = MoneyManager.Instance.EnemyCost(gameObject.name);
    }

    public void PlusCost()
    {
        MoneyManager.Instance.UpdateMoney(enemyCost);
    }

    public void UpdateCost(int value)
    {
        enemyCost += value;
    }
}
