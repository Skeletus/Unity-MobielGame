using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : Compositor
{
    protected override NodeResult Update()
    {
        NodeResult result = GetCurrentChild().UpdateNode();
        if (result == NodeResult.Failure)
        {
            return NodeResult.Failure;
        }

        if (result == NodeResult.Sucess)
        {
            if (Next())
                return NodeResult.InProgress;
            else
                return NodeResult.Sucess;
        }

        return NodeResult.InProgress;
    }
}
