using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_RemoveBlackboard_Data : BT_Node
{
    BehaviorTree tree;
    string keyToRemove;
    public BT_Task_RemoveBlackboard_Data(BehaviorTree tree, string keyToRemove)
    {
        this.tree = tree;
        this.keyToRemove = keyToRemove;
    }

    protected override NodeResult Execute()
    {
        if (tree != null && tree.Blackboard != null)
        {
            tree.Blackboard.RemoveBlackboardData(keyToRemove);
            return NodeResult.Sucess;
        }
        return NodeResult.Failure;
    }
}
