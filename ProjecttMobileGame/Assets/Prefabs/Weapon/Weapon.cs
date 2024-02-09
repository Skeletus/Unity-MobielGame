using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string attachedSlotTag;
    [SerializeField] float attackRateMultiplier =  1f;
    [SerializeField] AnimatorOverrideController overrideController;

    [SerializeField] AudioClip WeaponAudio;
    [SerializeField] float volume = 1f;
    AudioSource WeaponAudioSource;
    private void Awake()
    {
        WeaponAudioSource = GetComponent<AudioSource>();
    }

    public void PlayWeaponAudio()
    {
        WeaponAudioSource.PlayOneShot(WeaponAudio, volume);
    }

    public abstract void Attack();

    public string GetAttachedSlotTag()
    {
        return attachedSlotTag;
    }
    public GameObject owner
    {
        get;
        private set;
    }

    public void Init(GameObject _owner)
    {
        owner = _owner;
        UnEquip();
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        owner.GetComponent<Animator>().runtimeAnimatorController = overrideController;
        owner.GetComponent<Animator>().SetFloat("attackRateMultiplier", attackRateMultiplier);
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    public void DamageGameObject(GameObject gameObjectToDamage, float amount)
    {
        HealthComponent healthComponent = gameObjectToDamage.GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.ChangeHealth(-amount, owner);
        }
    }
}
