using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Compositor
{
    protected override NodeResult Update()
    {
        NodeResult result = GetCurrentChild().UpdateNode();
        if (result == NodeResult.Sucess)
        {
            return NodeResult.Sucess;
        }

        if (result == NodeResult.Failure)
        {
            if (Next())
                return NodeResult.InProgress;
            else
                return NodeResult.Failure;
        }

        return NodeResult.InProgress;
    }
}
