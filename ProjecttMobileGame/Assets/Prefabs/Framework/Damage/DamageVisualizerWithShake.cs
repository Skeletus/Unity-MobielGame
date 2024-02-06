using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualizerWithShake : DamageVisualiser
{
    [SerializeField] Shaker shaker;
    protected override void TookDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        base.TookDamage(health, delta, maxHealth, Instigator);
        if (shaker != null)
        {
            shaker.StartShake();
        }
    }
}
