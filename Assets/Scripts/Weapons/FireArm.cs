using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : Weapon<FireArmData>
{
    private const float FLASH_EFFECT_DURATION = 0.1f;

    [SerializeField] Transform firingPoint;
    [SerializeField] ParticleSystem muzzleFlash;
    private bool isRecoiling;
    private Coroutine coroutine;

    private void Fire()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(PlayMuzzleFlashEffect(FLASH_EFFECT_DURATION));   
        
        RaycastHit hitInfo;
        if(Physics.Raycast(firingPoint.position, firingPoint.forward, out hitInfo, weaponData.BulletRange))
        {
            IDamagable damageableObject = hitInfo.transform.GetComponent<IDamagable>();
            damageableObject?.TakeDamage(weaponData.DamageXP);
        }
    }

    public override void Use()
    {
        if (!isRecoiling)
        {
            Fire();
            isRecoiling = true;
            Invoke("ResetGun", 1/weaponData.RateOfFire);
        }    
    }

    private void ResetGun()
    {
        isRecoiling = false;
    }

    public bool HasRapidFire()
    {
        return weaponData.HasRapidFire;
    }

    IEnumerator PlayMuzzleFlashEffect(float duration)
    {
        muzzleFlash.Play(true);
        yield return new WaitForSeconds(duration);
        muzzleFlash.Stop();
    }

    public override WeaponData GetWeaponData()
    {
        return weaponData;
    }
}
