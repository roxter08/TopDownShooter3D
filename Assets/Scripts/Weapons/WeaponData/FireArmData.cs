using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Weapon/Create FireArm")]
public class FireArmData : WeaponData
{
    [SerializeField]private float bulletRange;
    [SerializeField]private bool hasRapidFire;
    [Range(1,10)]
    [SerializeField]private float rateOfFire;

    public float BulletRange => bulletRange;
    public bool HasRapidFire => hasRapidFire;
    public float RateOfFire => rateOfFire;

}
