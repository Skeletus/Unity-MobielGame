using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, TeamInterface
{
    [SerializeField] float FlightHeight;
    [SerializeField] Rigidbody rigidBody;

    int TeamID = -1;

    public void Launch(GameObject instigator, Vector3 Destination)
    {
        TeamInterface instigatorTeamInterface = instigator.GetComponent<TeamInterface>();
        if (instigatorTeamInterface != null)
        {
            TeamID = instigatorTeamInterface.GetTeamID();
        }
        float gravity = Physics.gravity.magnitude;
        float halfFlightTime = Mathf.Sqrt((FlightHeight * 2.0f) / gravity);

        Vector3 DestinationVec = Destination - transform.position;
        DestinationVec.y = 0;
        float horizontalDist = DestinationVec.magnitude;

        float upSpeed = halfFlightTime * gravity;
        float fwdSpeed = horizontalDist / (2.0f * halfFlightTime);

        Vector3 flightVel = Vector3.up * upSpeed + DestinationVec.normalized * fwdSpeed;
        rigidBody.AddForce(flightVel, ForceMode.VelocityChange);
    }

    public int GetTeamID() { return TeamID; }
}
