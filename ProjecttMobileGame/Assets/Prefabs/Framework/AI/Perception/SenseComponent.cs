using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SenseComponent;

public abstract class SenseComponent : MonoBehaviour
{
    [SerializeField] float forgettingTime = 4f;
    static List<PerceptionStimulus> registeredStimulusList = new List<PerceptionStimulus>();
    List<PerceptionStimulus> perceptibleStimulusList = new List<PerceptionStimulus>();

    Dictionary<PerceptionStimulus, Coroutine> forgettingRoutines = new Dictionary<PerceptionStimulus, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimulus stimulus, bool successFullySensed);

    public event OnPerceptionUpdated onPerceptionUpdated;

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
                    if (forgettingRoutines.TryGetValue(stimulus, out Coroutine routine))
                    {
                        StopCoroutine(routine);
                        forgettingRoutines.Remove(stimulus);
                    }
                    else
                    {
                        onPerceptionUpdated?.Invoke(stimulus, true);
                        Debug.Log($"Sensed {stimulus.gameObject}");
                    }
                }
            }
            else
            {
                if (perceptibleStimulusList.Contains(stimulus))
                {
                    perceptibleStimulusList.Remove(stimulus);
                    forgettingRoutines.Add(stimulus, StartCoroutine(ForgetStimulus(stimulus)));
                }
            }
        }
    }

    internal void AssignPerceivedStimuli(PerceptionStimulus targetStimuli)
    {
        perceptibleStimulusList.Add(targetStimuli);
        onPerceptionUpdated?.Invoke(targetStimuli, true);

        //TODO: WHAT IF WE ARE FORETTING IT.
        if (forgettingRoutines.TryGetValue(targetStimuli, out Coroutine forgetCoroutine))
        {
            StopCoroutine(forgetCoroutine);
            forgettingRoutines.Remove(targetStimuli);
        }
    }

    IEnumerator ForgetStimulus(PerceptionStimulus stimulus)
    {
        yield return new WaitForSeconds(forgettingTime);
        forgettingRoutines.Remove(stimulus);
        onPerceptionUpdated?.Invoke(stimulus, false);
        Debug.Log($"Track off {stimulus.gameObject}");
    }

    protected virtual void DrawDebug()
    {

    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
