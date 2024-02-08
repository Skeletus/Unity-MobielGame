using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour, IPurchaseListener, IRewardListener
{
    [SerializeField] Ability[] InitialAbilities;

    public delegate void OnNewAbilityAdded(Ability newAbility);
    public delegate void OnStaminaChange(float newAmount, float maxAmount);

    private List<Ability> abilities = new List<Ability>();

    public event OnNewAbilityAdded onNewAbilityAdded;
    public event OnStaminaChange onStaminaChange;

    [SerializeField] float stamina = 200f;
    [SerializeField] float maxStamina = 200f;

    public void BroadcastStaminaChangeImmedietely()
    {
        onStaminaChange?.Invoke(stamina, maxStamina);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Ability ability in InitialAbilities)
        {
            GiveAbility(ability);
        }
    }

    void GiveAbility(Ability ability)
    {
        Ability newAbility = Instantiate(ability);
        newAbility.InitAbility(this);
        abilities.Add(newAbility);
        onNewAbilityAdded?.Invoke(newAbility);
    }


    public void ActivateAbility(Ability abilityToActivate)
    {
        if (abilities.Contains(abilityToActivate))
        {
            abilityToActivate.ActivateAbility();
        }
    }

    float GetStamina()
    {
        return stamina;
    }

    public bool TryConsumeStamina(float staminaToConsume)
    {
        if (stamina <= staminaToConsume) return false;

        stamina -= staminaToConsume;
        BroadcastStaminaChangeImmedietely();
        return true;
    }

    public bool HandlePurchase(Object newPurchase)
    {
        Ability itemAsAbility = newPurchase as Ability;
        if (itemAsAbility == null) return false;

        GiveAbility(itemAsAbility);

        return true;
    }

    public void Reward(Reward reward)
    {
        stamina = Mathf.Clamp(stamina + reward.staminaReward, 0, maxStamina);
        BroadcastStaminaChangeImmedietely();
    }
}
