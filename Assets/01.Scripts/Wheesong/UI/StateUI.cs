using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateUI : MonoBehaviour
{
    [Header("UnitUI")]
    [SerializeField] private Button[] unitBtn;
    [SerializeField] private Transform unitImages;

    [Header("StateUI")]
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private Image unitSpriteImage;

    const float atkSp_value = 2f;

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
    }

    private void OnState(AgentData agentData)
    {
        unitNameText.text = agentData.name;
        unitSpriteImage.sprite = agentData.Sprite;
        stateText.text =
            $"HP : {agentData.hp} \n" +
            $"SP : {agentData.Speed} \n" +
            $"ATK : {agentData.attack} \n" +
            $"ATK_SP : {Mathf.Floor((atkSp_value / agentData.AttackDelay) * 100f)} \n" +
            $"RAN : {agentData.Range} \n";
    }
}
