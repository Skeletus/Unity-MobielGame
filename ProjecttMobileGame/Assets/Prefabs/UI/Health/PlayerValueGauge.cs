using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueGauge : MonoBehaviour
{
    [SerializeField] Image AmtImage;
    [SerializeField] TextMeshProUGUI AmtText;

    internal void UpdateValue(float health, float delta, float maxHealth)
    {
        AmtImage.fillAmount = health / maxHealth;
        AmtText.SetText(health.ToString());
        int healthAsInt = (int)health;
        AmtText.SetText(healthAsInt.ToString());
    }
}
