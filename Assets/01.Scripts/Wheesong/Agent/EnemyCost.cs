using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCost : MonoBehaviour
{
    public int enemyCost { get; private set; }

    public void OnEnable()
    {
        enemyCost = MoneyManager.Instance.EnemyCost(gameObject.name);
    }

    public void PlusCost()
    {
        MoneyManager.Instance.UpdateMoney(enemyCost);
        Debug.Log($"{gameObject.name} cost : {enemyCost}");
    }

    public void UpdateCost(int value)
    {
        enemyCost += value;
    }
}
