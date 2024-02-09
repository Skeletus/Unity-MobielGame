using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IRewardListener
{
    public delegate void OnHealthChange(float health, float delta, float maxHealth);
    public delegate void OnTakeDamage(float health, float delta, float maxHealth, GameObject Instigator);
    public delegate void OnHealthEmpty(GameObject Killer);

    [SerializeField] float health = 100;
    [SerializeField] float maxHealth = 100;

    public event OnHealthChange onHealthChange;
    public event OnTakeDamage onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    [Header("Audio")]
    [SerializeField] AudioClip HitAudio;
    [SerializeField] AudioClip DeathAudio;
    [SerializeField] float volume;

    public void ChangeHealth(float amount, GameObject instigator)
    {
        if (amount == 0 || health == 0)
        {
            return;
        }

        health += amount;

        if(amount < 0)
        {
            onTakeDamage?.Invoke(health, amount, maxHealth, instigator);
            Vector3 loc = transform.position;
            GameplayStatics.PlayAudioAtLoc(HitAudio, loc, 1);
        }

        onHealthChange?.Invoke(health, amount, maxHealth);

        if(health <= 0)
        {
            health = 0;
            onHealthEmpty?.Invoke(instigator);
            Vector3 loc = transform.position;
            GameplayStatics.PlayAudioAtLoc(DeathAudio, loc, 1);
        }

        //Debug.Log($"{gameObject.name}, taking damage {amount}, health is now: {health}");
    }

    public void BroadcastHealthValueImmeidately()
    {
        onHealthChange?.Invoke(health, 0, maxHealth);
    }

    public void Reward(Reward reward)
    {
        health = Mathf.Clamp(health + reward.healthReward, 0, maxHealth);
        onHealthChange?.Invoke(health, reward.healthReward, maxHealth);
    }
}
