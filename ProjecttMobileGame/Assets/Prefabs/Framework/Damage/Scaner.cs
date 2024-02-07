using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] Transform ScanerPivot;

    public delegate void OnScanDetectionUpdated(GameObject newDetection);

    public event OnScanDetectionUpdated onScanDetectionUpdated;

    [SerializeField] float scanRange;
    [SerializeField] float scaneDuration;

    internal void SetScanRange(float scanRange)
    {
        this.scanRange = scanRange;
    }

    internal void SetScanDuration(float duration)
    {
        scaneDuration = duration;
    }

    internal void AddChildAttached(Transform newChild)
    {
        newChild.parent = ScanerPivot;
        newChild.localPosition = Vector3.zero;
    }

    internal void StartScan()
    {
        ScanerPivot.localScale = Vector3.zero;
        StartCoroutine(StartScanCoroutine());
    }

    IEnumerator StartScanCoroutine()
    {
        float scanGrowthRate = scanRange / scaneDuration;
        float startTime = 0;
        while (startTime < scaneDuration)
        {
            startTime += Time.deltaTime;
            ScanerPivot.localScale += Vector3.one * scanGrowthRate * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        onScanDetectionUpdated?.Invoke(other.gameObject);
    }
}
