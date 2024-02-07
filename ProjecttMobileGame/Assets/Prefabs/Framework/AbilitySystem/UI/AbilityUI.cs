using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image AbilityIcon;
    [SerializeField] Image CooldownWheel;

    [SerializeField] float highlightSize = 1.5f;
    [SerializeField] float hightOffset = 200f;
    [SerializeField] float ScaleSpeed = 20f;
    [SerializeField] RectTransform OffsetPivot;

    Vector3 GoalScale = Vector3.one;
    Vector3 GoalOffset = Vector3.zero;

    bool bIsOnCooldown = false;
    float CooldownCounter = 0f;

    public void SetScaleAmt(float amt)
    {
        GoalScale = Vector3.one * (1 + (highlightSize - 1) * amt);
        GoalOffset = Vector3.left * hightOffset * amt;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale = Vector3.Lerp(transform.localScale, GoalScale, Time.deltaTime * ScaleSpeed);
        OffsetPivot.localPosition = Vector3.Lerp(OffsetPivot.localPosition, GoalOffset, Time.deltaTime * ScaleSpeed);
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
