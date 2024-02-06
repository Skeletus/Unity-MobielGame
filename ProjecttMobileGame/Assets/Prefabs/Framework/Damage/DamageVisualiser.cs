using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualiser : MonoBehaviour
{
    [SerializeField] Renderer mesh;
    [SerializeField] Color DamageEmmisionColor;
    [SerializeField] float BlinkSpeed = 2f;
    [SerializeField] string EmmisionColorPropertyName = "_Addtion";
    [SerializeField] HealthComponent healthComponent;
    Color OrigionalEmissionColor;

    // Start is called before the first frame update
    void Start()
    {
        Material mat = mesh.material;
        mesh.material = new Material(mat);

        OrigionalEmissionColor = mesh.material.GetColor(EmmisionColorPropertyName);
        healthComponent.onTakeDamage += TookDamage;
    }

    protected virtual void TookDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        Color currentEmmisionColor = mesh.material.GetColor(EmmisionColorPropertyName);
        if (Mathf.Abs((currentEmmisionColor - OrigionalEmissionColor).grayscale) < 0.1f)
        {
            mesh.material.SetColor(EmmisionColorPropertyName, DamageEmmisionColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color currentEmmisionColor = mesh.material.GetColor(EmmisionColorPropertyName);
        Color newEmmisionColor = Color.Lerp(currentEmmisionColor, OrigionalEmissionColor, Time.deltaTime * BlinkSpeed);
        mesh.material.SetColor(EmmisionColorPropertyName, newEmmisionColor);
    }
}
