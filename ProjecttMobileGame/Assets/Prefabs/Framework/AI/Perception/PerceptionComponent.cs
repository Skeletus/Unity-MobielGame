using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] SenseComponent[] sensesArray;
    LinkedList<PerceptionStimulus> currentlyPerceptibleStimulusList = new LinkedList<PerceptionStimulus>();
    PerceptionStimulus targetStimulus;

    public delegate void OnPerceptionTargetChanged(GameObject target, bool sensed);
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;

    // Start is called before the first frame update
    private void Awake()
    {
        foreach(SenseComponent sense in sensesArray)
        {
            sense.onPerceptionUpdated += SenseUpdated;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void SenseUpdated(PerceptionStimulus stimulus, bool successFullySensed)
    {
        var nodeFound = currentlyPerceptibleStimulusList.Find(stimulus);
        if (successFullySensed)
        {

            if (nodeFound != null)
            {
                currentlyPerceptibleStimulusList.AddAfter(nodeFound, stimulus);
            }
            else
            {
                currentlyPerceptibleStimulusList.AddLast(stimulus);
            }
        }
        else
        {
            currentlyPerceptibleStimulusList.Remove(nodeFound);
        }

        if(currentlyPerceptibleStimulusList.Count != 0)
        {
            PerceptionStimulus highestStimulus = currentlyPerceptibleStimulusList.First.Value;
            if (targetStimulus == null || targetStimulus != highestStimulus)
            {
                targetStimulus = highestStimulus;
                onPerceptionTargetChanged?.Invoke(targetStimulus.gameObject, true);
            }
        }
        else
        {
            if (targetStimulus != null)
            {
                onPerceptionTargetChanged?.Invoke(targetStimulus.gameObject, false);
                targetStimulus = null;
            }
        }
    }

    internal void AssignPercievedStimui(PerceptionStimulus targetStimuli)
    {
        if (sensesArray.Length != 0)
        {
            sensesArray[0].AssignPerceivedStimuli(targetStimuli);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
