using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StateUI : MonoBehaviour
{
    [Header("UnitUI")]
    [SerializeField] private Button[] unitBtn;
    [SerializeField] private Transform unitImages;

    [Header("StateUI")]
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI unitLevelText;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private Animator levelupEffect;
    [SerializeField] private Image unitSpriteImage;
    [SerializeField] private Button unitUpdgradeBtn;

    int upgradeCost;
    const float atkSp_value = 2f;
    const float hpUpgradeGrape = 1.3f;
    const float attackUpgradeGrape = 1.2f;

    private void Start()
    {
        for (int i = 0; i < unitImages.childCount; i++)
        {
            AgentData unit = Resources.Load<AgentData>($"UnitSO/" +
                $"{unitImages.GetChild(i).name.Replace("_Image", "")}");
            unitBtn[i].transform.GetChild(0).GetComponent<Image>()
                .sprite = unit.Sprite;
            unitBtn[i].onClick.AddListener(() => OnState(unit));
        }
        unitBtn[0].onClick?.Invoke();
    }

    private void OnState(AgentData agentData)
    {
        unitUpdgradeBtn.onClick.RemoveAllListeners();
        unitUpdgradeBtn.onClick.AddListener(() => { UnitDataUpgrade(agentData.name); });

        unitSpriteImage.sprite = agentData.Sprite;
        unitNameText.text = agentData.name;
        unitLevelText.text = $"LV.{agentData.level}";
        stateText.text =
            $"HP : {agentData.hp} \n" +
            $"SP : {agentData.Speed} \n" +
            $"ATK : {agentData.attack} \n" +
            $"ATK_SP : {Mathf.Floor((atkSp_value / agentData.AttackDelay) * 1f)}";

        upgradeCost = Mathf.RoundToInt(MoneyManager.Instance.UnitCost(agentData.name) * 2.5f);
        unitUpdgradeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().
            text = upgradeCost.ToString();
    }

    private void UnitDataUpgrade(string unitName)//체력 30%, 공격력 20% 증가
    {
        if (!MoneyManager.Instance.EnoughMoney(upgradeCost)) return;
        MoneyManager.Instance.UpdateMoney(-upgradeCost);

        AgentData unitData = AgentDictionary.Instance.unitDatas[unitName];
        MoneyManager.Instance.UpdateUnitCost(unitName, unitData.cost / 3);
        unitData.cost += (unitData.cost / 3);
        unitData.level++;
        unitData.hp *= hpUpgradeGrape;
        unitData.attack *= attackUpgradeGrape;

        levelupEffect.SetTrigger("Effect");

        OnState(unitData);
    }
}
