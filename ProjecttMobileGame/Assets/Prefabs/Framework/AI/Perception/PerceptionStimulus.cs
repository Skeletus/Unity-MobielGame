using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionStimulus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SenseComponent.RegisterStimulus(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        SenseComponent.UnRegisterStimulus(this);
    }
}
