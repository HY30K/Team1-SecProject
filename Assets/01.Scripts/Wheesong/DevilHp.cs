using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DevilHp : MonoBehaviour
{
    private Slider devilSlsider;
    private float devilMaxhp;
    public float devilHp;
    float a = 10;

    private void Awake()
    {
        devilSlsider = GetComponent<Slider>();
        devilMaxhp = devilSlsider.maxValue;
        devilHp = devilMaxhp;
    }

    public void OnHit(float dmg)
    {
        float nDmg = devilHp - dmg;
        DOTween.To(() => devilHp, x => devilHp = x, nDmg, 1f).SetEase(Ease.OutCubic);
        DOTween.To(() => devilSlsider.value, x => devilSlsider.value = x, nDmg, 1f).SetEase(Ease.OutCubic);
    }

    public void OnHeel(float heel)
    {

    }
}
