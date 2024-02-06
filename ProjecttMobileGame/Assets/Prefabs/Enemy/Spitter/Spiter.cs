using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiter : Enemy
{
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
    }

    public void Shoot()
    {
        Debug.Log("Spitter shooting!");
    }
}
