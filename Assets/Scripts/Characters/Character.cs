using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour , ICommonCallbackProvider , IDamagable
{
    [SerializeField] private int maxHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;
    private Transform healthPoint;
    private int healthXP;

    protected Transform weaponHolder;
    
    public Transform HealthPoint => healthPoint;
    public Action OnHealthUpdated = delegate { };

    protected CommonCallbacks commonCallback;
    

    protected abstract void Move();
    protected abstract void Turn();

    protected virtual void Start()
    {
        weaponHolder = transform.Find("WeaponHolder");
        healthPoint = transform.Find("HealthbarPoint");
        healthXP = maxHealth;
        commonCallback.OnCharacterAddedToGame(this);
    }
    

    protected virtual void IncreaseHealth(int value)
    {
        healthXP = Mathf.Min(healthXP + value, maxHealth);
    }

    protected virtual void DecreaseHealth(int value)
    {
        healthXP = Mathf.Max(0, healthXP - value);
        if (healthXP <= 0)
        {
            Kill();
        }
    }

    protected virtual void Kill()
    {
        commonCallback.OnCharacterRemovedFromGame(this);
        Destroy(gameObject);
    }

    public void SetCallback(CommonCallbacks callback)
    {
        commonCallback = callback;
    }

    public int GetCurrentHealth()
    {
        return healthXP;
    }
    public float GetNormalizedHealth()
    {
        return (float)healthXP / (float)maxHealth;
    }

    public virtual void TakeDamage(int damageXP)
    {
        DecreaseHealth(damageXP);
        OnHealthUpdated();
    }
}
