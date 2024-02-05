using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_AttackTarget : BT_Node
{
    BehaviorTree tree;
    string targetKey;
    GameObject target;
    public BT_Task_AttackTarget(BehaviorTree tree, string targetKey)
    {
        this.tree = tree;
        this.targetKey = targetKey;
    }
    protected override NodeResult Execute()
    {
        if (!tree || tree.Blackboard == null || !tree.Blackboard.GetBlackboardData(targetKey, out target))
            return NodeResult.Failure;

        BehaviorTreeInterface BehaviorInterface = tree.GetBehaviorTreeInterface();
        if (BehaviorInterface == null)
            return NodeResult.Failure;

        BehaviorInterface.AttackTarget(target);
        return NodeResult.Sucess;
    }
}
