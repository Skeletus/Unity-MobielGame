using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : Ability
{
    [SerializeField] Scaner ScanerPrefab;
    [SerializeField] float fireRadius;
    [SerializeField] float fireDuration;

    [SerializeField] GameObject scanVFX;
    [SerializeField] GameObject DamageVFX;

    public override void ActivateAbility()
    {
        if (!CommitAbility()) return;
        Scaner fireScaner = Instantiate(ScanerPrefab, AbilityComp.transform);
        fireScaner.SetScanRange(fireRadius);
        fireScaner.SetScanDuration(fireDuration);
        fireScaner.AddChildAttached(Instantiate(scanVFX).transform);
        fireScaner.onScanDetectionUpdated += DetectionUpdate;
        fireScaner.StartScan();
    }

    private void DetectionUpdate(GameObject newDetection)
    {
        Debug.Log($"Detected: {newDetection.name}");
    }
}
