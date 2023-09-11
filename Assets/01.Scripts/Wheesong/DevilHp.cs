using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevilHp : MonoBehaviour
{
    private Slider devilSlsider;
    private float devilMaxhp;
    private float devilHp;

    private void Awake()
    {
        devilSlsider = GetComponent<Slider>();
        devilMaxhp = devilSlsider.maxValue;
        devilHp = devilMaxhp;
    }

    public void OnHit(float dmg)
    {
        devilHp = Mathf.Lerp(devilHp, devilHp - dmg, 0.1f);
        devilSlsider.value = devilHp;
    }
}
