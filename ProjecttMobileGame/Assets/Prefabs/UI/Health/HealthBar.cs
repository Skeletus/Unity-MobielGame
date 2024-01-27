using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    private Transform attachedPoint;

    public void Init(Transform _attachedPoint)
    {
        attachedPoint = _attachedPoint;
    }

    public void SetHealthSliderValue(float health, float delta, float maxHealth)
    {
        healthSlider.value = health / maxHealth;
    }

    internal void OnOwnerDead()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector3 attachedScreenPoint = Camera.main.WorldToScreenPoint(attachedPoint.position);
        transform.position = attachedScreenPoint;
    }
}
