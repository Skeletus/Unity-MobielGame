using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_Wait : BT_Node
{
    float WaitTime = 2f;

    float timeElapsed = 0f;

    public BT_Task_Wait(float WaitTime)
    {
        this.WaitTime = WaitTime;
    }

    protected override NodeResult Execute()
    {
        if (WaitTime <= 0)
        {
            return NodeResult.Sucess;
        }
        Debug.Log($"wait started with duration: {WaitTime}");
        timeElapsed = 0f;
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= WaitTime)
        {
            Debug.Log("Wait finished");
            return NodeResult.Sucess;
        }
        Debug.Log($"Waiting for {timeElapsed}");
        return NodeResult.InProgress;
    }
}
