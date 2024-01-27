using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : SenseComponent
{
    [SerializeField] float sightDistance = 5f;
    [SerializeField] float sightHalfAngle = 5f;
    [SerializeField] float sightHeight = 1f;
    protected override bool IsStimulusSensable(PerceptionStimulus stimulus)
    {
        float distance = Vector3.Distance(stimulus.transform.position, transform.position);
        if( distance > sightDistance)
        {
            return false;
        }

        Vector3 forwardDirection = transform.forward;
        Vector3 stimulusDirection = (stimulus.transform.position - transform.position).normalized;

        if (Vector3.Angle(forwardDirection, stimulusDirection) > sightHalfAngle)
        {
            return false;
        }

        if ( Physics.Raycast(transform.position + Vector3.up * sightHeight, stimulusDirection, out RaycastHit hitInfo, sightDistance))
        {
            if (hitInfo.collider.gameObject != stimulus.gameObject)
            {
                return false;
            }
        }

        return true;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Vector3 drawCenter = transform.position + Vector3.up * sightHeight;
        Gizmos.DrawWireSphere(drawCenter, sightDistance);

        Vector3 leftLimitDirection = Quaternion.AngleAxis(sightHalfAngle, Vector3.up) * transform.forward;
        Vector3 rightLimitDirection = Quaternion.AngleAxis(-sightHalfAngle, Vector3.up) * transform.forward;

        Gizmos.DrawLine(drawCenter, drawCenter + leftLimitDirection * sightDistance);
        Gizmos.DrawLine(drawCenter, drawCenter + rightLimitDirection * sightDistance);
    }
}
