using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float FlightHeight;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] DamageComponent DamageComponent;
    [SerializeField] ParticleSystem ExplosionVFX;

    TeamInterface instigatorTeamInterface;

    public void Launch(GameObject instigator, Vector3 Destination)
    {
        instigatorTeamInterface = instigator.GetComponent<TeamInterface>();
        if (instigatorTeamInterface != null)
        {
            DamageComponent.SetTeamInterfaceSrc(instigatorTeamInterface);
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

    private void OnTriggerEnter(Collider other)
    {
        if (instigatorTeamInterface.GetRelationTowards(other.gameObject) != ETeamRelation.Friendly)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Vector3 pawnPos = transform.position;
        Instantiate(ExplosionVFX, pawnPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
