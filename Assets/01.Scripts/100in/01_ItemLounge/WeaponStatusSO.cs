using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/WeaponStatusSO")]
public class WeaponStatusSO : ScriptableObject
{
    [SerializeField] private string _weaponName; public string WeaponName { get { return _weaponName; } }
    [SerializeField] private Sprite _weaponSprite; public Sprite WeaponSprite { get { return _weaponSprite; } }
    [SerializeField] private float _keyValue; public float keyValue { get { return _keyValue; } }
    [SerializeField] private float _hp; public float Hp { get { return _hp; } }
    [SerializeField] private float _speed; public float Speed { get { return _speed; } }
    [SerializeField] private float _attackPower; public float AttackPower { get { return _attackPower; } }
    [SerializeField] private float _attackSpeed; public float AttackSpeed { get { return _attackSpeed; } }
}
