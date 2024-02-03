using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComponent : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    int currentPatrolPointIndex = -1;

    public bool GetNextPatrolPoint(out Vector3 point)
    {
        point = Vector3.zero;
        if (patrolPoints.Length == 0) return false;
        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        point = patrolPoints[currentPatrolPointIndex].position;
        return true;
    }
}
