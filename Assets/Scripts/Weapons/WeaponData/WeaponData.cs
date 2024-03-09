using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponData : ScriptableObject
{
    [SerializeField]private string weaponName;
    [SerializeField]private int damageXP;
    [SerializeField]private Sprite icon;

    public string Name => weaponName;
    public int DamageXP => damageXP;
    public Sprite Icon => icon;

}
