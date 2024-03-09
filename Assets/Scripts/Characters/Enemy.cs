using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    protected NavMeshAgent NavMeshAgent { get; private set; }
    [SerializeField] private float moveRange;

    protected override void Start()
    {
        base.Start();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = moveSpeed;
        NavMeshAgent.angularSpeed = turnSpeed;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected override void Move()
    {
        Wander();
    }

    protected void Wander()
    {
        if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(transform.position, moveRange, out point))
            {
                NavMeshAgent.SetDestination(point);
            }
        }
    }

    protected override void Turn()
    {
       
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
