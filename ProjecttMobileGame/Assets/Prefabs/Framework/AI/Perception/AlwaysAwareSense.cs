using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAwareSense : SenseComponent
{
    [SerializeField] float awareDistance = 2f;
    protected override bool IsStimulusSensable(PerceptionStimulus stimulus)
    {
        return Vector3.Distance(transform.position, stimulus.transform.position) <= awareDistance;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Gizmos.DrawWireSphere(transform.position + Vector3.up, awareDistance);
    }
}
