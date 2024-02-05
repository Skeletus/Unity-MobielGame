using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : Enemy
{

    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
    }
}
