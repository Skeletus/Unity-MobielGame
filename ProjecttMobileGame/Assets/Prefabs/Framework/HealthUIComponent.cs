using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour
{
    [SerializeField] HealthBar healthBarToSpawn;
    [SerializeField] Transform healthBarAttachedPoint;
    [SerializeField] HealthComponent healthComponent;

    private void Start()
    {
        InGameUI inGameUI = FindObjectOfType<InGameUI>();
        HealthBar newHealthBar = Instantiate(healthBarToSpawn, inGameUI.transform);
        newHealthBar.Init(healthBarAttachedPoint);
        healthComponent.onHealthChange += newHealthBar.SetHealthSliderValue;
        healthComponent.onHealthEmpty += newHealthBar.OnOwnerDead;
    }
}
