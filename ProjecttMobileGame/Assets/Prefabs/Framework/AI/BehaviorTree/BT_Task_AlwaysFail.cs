using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_AlwaysFail : BT_Node
{
    protected override NodeResult Execute()
    {
        Debug.Log("Failed");
        return NodeResult.Failure;
    }
}
