using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_Log : BT_Node
{
    string message;
    public BT_Task_Log(string message)
    {
        this.message = message;
    }
    protected override NodeResult Execute()
    {
        //Debug.Log(message);
        return NodeResult.Sucess;
    }
}
