using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSense : SenseComponent
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] float hitMemory;
    Dictionary<PerceptionStimulus, Coroutine> hitRecord = new Dictionary<PerceptionStimulus, Coroutine> ();

    protected override bool IsStimulusSensable(PerceptionStimulus stimulus)
    {
        return hitRecord.ContainsKey(stimulus);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthComponent.onTakeDamage += TookDamage;
    }

    private void TookDamage(float health, float delta, float maxHealth, GameObject instigator)
    {
        PerceptionStimulus stimulus = instigator.GetComponent<PerceptionStimulus>();
        if(stimulus != null)
        {
            Coroutine newForgettingCoroutine = StartCoroutine(ForgetStimulus(stimulus));
            if(hitRecord.TryGetValue(stimulus, out Coroutine onGoingCoroutine))
            {
                StopCoroutine(onGoingCoroutine);
                hitRecord[stimulus] = onGoingCoroutine;
            }
            else
            {
                hitRecord.Add(stimulus, newForgettingCoroutine);
            }
        }
    }

    IEnumerator ForgetStimulus(PerceptionStimulus stimulus)
    {
        yield return new WaitForSeconds(hitMemory);
        hitRecord.Remove(stimulus);
    }

}
