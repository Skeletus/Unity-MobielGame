using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComponent : MonoBehaviour
{
    static List<PerceptionStimulus> registeredStimulusList = new List<PerceptionStimulus>();
    List<PerceptionStimulus> perceptibleStimulusList = new List<PerceptionStimulus>();

    public static void RegisterStimulus(PerceptionStimulus stimulus)
    {
        if (registeredStimulusList.Contains(stimulus))
        {
            return;
        }
        registeredStimulusList.Add(stimulus);
    }

    public static void UnRegisterStimulus(PerceptionStimulus stimulus)
    {
        registeredStimulusList.Remove(stimulus);
    }

    protected abstract bool IsStimulusSensable(PerceptionStimulus stimulus);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var stimulus in registeredStimulusList)
        {
            if(IsStimulusSensable(stimulus))
            {
                if (!perceptibleStimulusList.Contains(stimulus))
                {
                    perceptibleStimulusList.Add(stimulus);
                    Debug.Log($"Sensed {stimulus.gameObject}");
                }
            }
            else
            {
                if (perceptibleStimulusList.Contains(stimulus))
                {
                    perceptibleStimulusList.Remove(stimulus);
                    Debug.Log($"Track off {stimulus.gameObject}");
                }
            }
        }
    }

    protected virtual void DrawDebug()
    {

    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
