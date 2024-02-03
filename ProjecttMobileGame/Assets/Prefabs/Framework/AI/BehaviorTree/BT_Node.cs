using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    Sucess,
    Failure,
    InProgress
}

public abstract class BT_Node
{
    public NodeResult UpdateNode()
    {
        if (!started)
        {
            started = true;
            NodeResult executeResult = Execute();
            if(executeResult != NodeResult.InProgress)
            {
                EndNode();
                return executeResult;
            }
        }

        NodeResult updateResult = Update();
        if(updateResult != NodeResult.InProgress)
        {
            EndNode();
        }
        return updateResult;

    }

    protected virtual NodeResult Execute()
    {
        return NodeResult.Sucess;
    }

    protected virtual NodeResult Update()
    {
        return NodeResult.Sucess;
    }

    protected virtual void End()
    {

    }

    private void EndNode()
    {
        started = false;
        End();
    }

    public void Abort()
    {
        EndNode();
    }

    bool started = false;
    int priority;

    public int GetPriority()
    {
        return priority;
    }

    public virtual void SortPriority(ref int priorityConter)
    {
        priority = priorityConter++;
        Debug.Log($"{this} has priorty {priority}");
    }
}
