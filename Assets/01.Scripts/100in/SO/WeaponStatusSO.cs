using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WeaponStatusSO")]
public class WeaponStatusSO : ScriptableObject
{
    [SerializeField] private string _weaponName; public string WeaponName { get { return _weaponName; } }
    [SerializeField] private Sprite _weaponSprite;public Sprite WeaponSprite { get { return _weaponSprite; } }
    [SerializeField] private int _keyValue; public int keyValue { get { return _keyValue; } }
    [SerializeField] private int _hp; public int Hp { get { return _hp; } }
    [SerializeField] private int _attackPower; public int AttackPower { get { return _attackPower; } }
    [SerializeField] private float _attackSpeed; public float AttackSpeed { get { return _attackSpeed; } }
}
