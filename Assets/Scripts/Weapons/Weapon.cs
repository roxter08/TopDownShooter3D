using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon<T> : Weapon where T : WeaponData
{
    [SerializeField]protected T weaponData;
}

public abstract class Weapon : MonoBehaviour
{
    private bool isActive;
    public bool IsActive => isActive;

    public abstract void Use();
    public void ActivateWeapon(bool value)
    {
        isActive = value;
        gameObject.SetActive(value);
    }

    public abstract WeaponData GetWeaponData();
}
