using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int money;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ChangeMoney(int value)
    {
        money += value;
    }

    public bool EnoughMoney(int value)
    {
        return money - value > 0 ? true : false;
    }
}
