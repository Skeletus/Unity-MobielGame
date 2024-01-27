using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] AimComponent aimComponent;
    [SerializeField] float damage = 5f;
    public override void Attack()
    {
        GameObject target = aimComponent.GetAimTarget();
        Debug.Log($"aiming at {target}");
        DamageGameObject(target, damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
