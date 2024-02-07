using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image AbilityIcon;
    [SerializeField] Image CooldownWheel;

    bool bIsOnCooldown = false;
    float CooldownCounter = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void Init(Ability newAbility)
    {
        ability = newAbility;
        AbilityIcon.sprite = newAbility.GetAbilityIcon();
        CooldownWheel.enabled = false;
        ability.onCooldownStarted += StartCooldown;
    }

    private void StartCooldown()
    {

        if (bIsOnCooldown) return;

        StartCoroutine(CooldownCoroutine());
    }

    internal void ActivateAbility()
    {
        ability.ActivateAbility();
    }

    IEnumerator CooldownCoroutine()
    {
        bIsOnCooldown = true;
        CooldownCounter = ability.GetCooldownDuration();
        float cooldownDuration = CooldownCounter;
        CooldownWheel.enabled = true;

        while (CooldownCounter > 0)
        {
            CooldownCounter -= Time.deltaTime;
            CooldownWheel.fillAmount = CooldownCounter / cooldownDuration;
            yield return new WaitForEndOfFrame();
        }

        bIsOnCooldown = false;
        CooldownWheel.enabled = false;
    }
}
