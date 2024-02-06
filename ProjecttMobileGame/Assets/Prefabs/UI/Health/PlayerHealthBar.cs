using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image AmtImage;
    [SerializeField] TextMeshProUGUI AmtText;

    internal void UpdateHealth(float health, float delta, float maxHealth)
    {
        AmtImage.fillAmount = health / maxHealth;
        AmtText.SetText(health.ToString());
    }
}
