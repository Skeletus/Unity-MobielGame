using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] AimComponent aimComponent;
    [SerializeField] float damage = 5f;
    [SerializeField] ParticleSystem bulletVFX;
    public override void Attack()
    {
        GameObject target = aimComponent.GetAimTarget(out Vector3 aimDirection);
        //Debug.Log($"aiming at {target}");
        DamageGameObject(target, damage);

        bulletVFX.transform.rotation = Quaternion.LookRotation(aimDirection);
        bulletVFX.Emit(bulletVFX.emission.GetBurst(0).maxCount);
        PlayWeaponAudio();
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
