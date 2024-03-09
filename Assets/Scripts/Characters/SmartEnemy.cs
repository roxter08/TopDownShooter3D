using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SmartEnemy : Enemy
{
    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;

    private GameObject targetPlayer;
    private Weapon currentWeapon;
    private enum EnemyState
    {
        Patrol,
        Chase,
        Shoot
    };

    private EnemyState currentState = EnemyState.Patrol;

    protected override void Start()
    {
        base.Start();
        targetPlayer = GameObject.FindWithTag("Player");
        currentWeapon = weaponHolder.GetComponentInChildren<Weapon>();
    }

    protected override void Move()
    {
        switch(currentState)
        {
            case EnemyState.Patrol:
                Wander();
                break;

            case EnemyState.Chase:
                ChaseTarget();
                break;
        }
    }

    protected override void Turn()
    {
        Vector3 targetPos = targetPlayer.transform.position;
        targetPos.y = transform.position.y;
        transform.forward = (targetPos - transform.position).normalized;
    }

    protected override void Update()
    {
        UpdateCurrentState();

        NavMeshAgent.isStopped = currentState == EnemyState.Shoot;

        if (currentState == EnemyState.Shoot)
        {
            Turn();
            currentWeapon.Use();
        }
        else
        {
            Move();
        }
    }


    //Update current state based on distance from Player
    private void UpdateCurrentState()
    {
        float distanceFromTarget = Vector3.Distance(transform.position, targetPlayer.transform.position);
        
        if(distanceFromTarget > chasingRange)
        {
            currentState = EnemyState.Patrol;
        }
        else if(distanceFromTarget <= chasingRange && distanceFromTarget > shootingRange)
        {
            currentState=EnemyState.Chase;
        }
        else if (distanceFromTarget <= shootingRange)
        {
            currentState = EnemyState.Shoot;
        }
    }


    private void ChaseTarget()
    {
        NavMeshAgent.SetDestination(targetPlayer.transform.position);
    }
}
