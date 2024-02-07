using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image AbilityIcon;
    [SerializeField] Image CooldownWheel;


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

    }
}
