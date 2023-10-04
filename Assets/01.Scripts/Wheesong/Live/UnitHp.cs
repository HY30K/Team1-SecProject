using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitHp : Living
{
    private Unit unit;
    private TMP_Text levelText;

    protected override void Awake()
    {
        base.Awake();
        unit = GetComponent<Unit>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        levelText = hpBar.transform.GetChild(1).GetComponent<TMP_Text>();
        levelText.text = $"Lv.{unit.level}";
       //levelText.rectTransform.localScale = new Vector2(0.2f, 1);
    }

    public override void Die()
    {
        base.Die();
        unit.state = State.DIE;
        unit.isChangeState = true;

        StartCoroutine(PushUnit(1));
    }

    IEnumerator PushUnit(float time)
    {
        yield return new WaitForSeconds(time);
        WaveSystem.Instance.DieUnit();
        PoolingManager.Instance.Push(gameObject);
    }
}
