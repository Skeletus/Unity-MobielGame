using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VFXSpec
{
    public ParticleSystem particleSystem;
    public float size;
}

public class Spawner : Enemy
{

    [SerializeField] VFXSpec[] DeathVFX;
    protected override void Dead()
    {
        foreach (VFXSpec spec in DeathVFX)
        {
            ParticleSystem particleSys = Instantiate(spec.particleSystem);
            particleSys.transform.position = transform.position;
            particleSys.transform.localScale = Vector3.one * spec.size;
        }
    }
}
