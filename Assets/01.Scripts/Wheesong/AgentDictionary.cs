using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDictionary : MonoBehaviour
{
    public static AgentDictionary Instance;

    public Dictionary<string, AgentData> unitDatas = new Dictionary<string, AgentData>();
    public Dictionary<string, AgentData> enemyDatas = new Dictionary<string, AgentData>();

    private void Awake()
    {
        if(Instance == null) Instance = this;

        AgentData[] unitDataArray = Resources.LoadAll<AgentData>("UnitSO");
        AgentData[] enemyDataArray = Resources.LoadAll<AgentData>("EnemySO");

        foreach (AgentData unitData in unitDataArray)
        {
            //새로 생성해서 넣어줘야 리소스 파일를 못 건들임
            AgentData newAgentData = Instantiate(unitData);
            newAgentData.name = unitData.name;
            unitDatas.Add(unitData.name, newAgentData);
        }

        foreach (AgentData enemyData in enemyDataArray)
        {
            AgentData newAgentData = Instantiate(enemyData);
            newAgentData.name = enemyData.name;
            enemyDatas.Add(enemyData.name, newAgentData);
        }
    }
}
