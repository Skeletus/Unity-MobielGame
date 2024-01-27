using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] float aimRange = 1000;
    [SerializeField] LayerMask aimMask;

    public GameObject GetAimTarget(out Vector3 aimDirection)
    {
        Vector3 aimStart = muzzle.position;
        aimDirection = GetAimDirection();

        if (Physics.Raycast(aimStart, GetAimDirection(), out RaycastHit hitInfo, aimRange, aimMask))
        {
            return hitInfo.collider.gameObject;  
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(muzzle.position, muzzle.position + GetAimDirection() * aimRange);
    }

    Vector3 GetAimDirection()
    {
        Vector3 muzzleDirection = muzzle.forward;
        return new Vector3(muzzleDirection.x, 0f, muzzleDirection.z).normalized;
    }
}
