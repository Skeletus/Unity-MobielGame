using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] AimComponent aimComponent;
    public override void Attack()
    {
        GameObject target = aimComponent.GetAimTarget();
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
