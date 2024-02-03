using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] float turnSpeed = 8;
    public float RotateTowards(Vector3 aimDirection)
    {
        float currentTurnSpeed = 0;
        if (aimDirection.magnitude != 0)
        {
            Quaternion previousRotation = transform.rotation;

            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), turnLerpAlpha);

            Quaternion currentRotation = transform.rotation;
            float direction = Vector3.Dot(aimDirection, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(previousRotation, currentRotation) * direction;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
        return currentTurnSpeed;
    }
}
