using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiter : Enemy
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform launchPoint;

    Vector3 Destination;

    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
        Destination = target.transform.position;
    }

    public void Shoot()
    {
        //Debug.Log("Spitter shooting!");
        Projectile newProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
        newProjectile.Launch(gameObject, Destination);
    }
}
