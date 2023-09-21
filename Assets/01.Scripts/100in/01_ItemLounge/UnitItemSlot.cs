using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitItemSlot : MonoBehaviour
{
    private Unit unit;
    private UnitHp unitHp;
    private List<SpriteRenderer> _itemSlot = new List<SpriteRenderer>();
    private int _maxSlot = 3;
    private int _currentSlot = 0;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        unitHp = GetComponent<UnitHp>();

        for (int i = 0; i < _maxSlot ; i++)
        {
            _itemSlot.Add(transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>());
            Debug.Log(_itemSlot[i]);
        }
    }
 
    public void AddItem(WeaponStatusSO weaponStatusSO)
    {
        _itemSlot[_currentSlot].sprite = weaponStatusSO.WeaponSprite;
        unitHp.OnHeel(weaponStatusSO.Hp);
        unit.speed += weaponStatusSO.Speed;
        unit.attack += weaponStatusSO.AttackPower;
        unit.attackDelay *= weaponStatusSO.AttackSpeed;
        _currentSlot++;
    }
    public bool Check()
    {
        if (_currentSlot > 2)
        {
          
            return false;
        }
        else
        {
           
            return true;
        }
    }
}
