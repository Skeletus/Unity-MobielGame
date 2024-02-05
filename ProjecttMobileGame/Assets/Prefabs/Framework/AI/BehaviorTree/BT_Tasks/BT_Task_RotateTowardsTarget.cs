using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_RotateTowardsTarget : BT_Node
{
    BehaviorTree tree;
    string targetKey;
    float acceptableDegrees;
    GameObject target;
    BehaviorTreeInterface behaviorTreeInterface;

    public BT_Task_RotateTowardsTarget(BehaviorTree tree, string targetKey, float acceptableDegrees = 10f)
    {
        this.tree = tree;
        this.targetKey = targetKey;
        this.acceptableDegrees = acceptableDegrees;

        this.behaviorTreeInterface = tree.GetBehaviorTreeInterface();
    }

    protected override NodeResult Execute()
    {
        if (tree == null || tree.Blackboard == null)
            return NodeResult.Failure;

        if (behaviorTreeInterface == null)
            return NodeResult.Failure;

        if (!tree.Blackboard.GetBlackboardData(targetKey, out target))
            return NodeResult.Failure;

        if (IsInAcceptableDegrees())
            return NodeResult.Sucess;

        tree.Blackboard.onBlackboardValueChange += BlacKboardValueChanged;

        return NodeResult.InProgress;
    }

    private void BlacKboardValueChanged(string key, object val)
    {
        if (key == targetKey)
        {
            target = (GameObject)val;
        }
    }

    protected override NodeResult Update()
    {
        if (target == null)
            return NodeResult.Failure;
        if (IsInAcceptableDegrees())
            return NodeResult.Sucess;

        behaviorTreeInterface.RotateTowards(target);
        return NodeResult.InProgress;
    }

    bool IsInAcceptableDegrees()
    {
        if (target == null) return false;
        Vector3 targetDir = (target.transform.position - tree.transform.position).normalized;
        Vector3 dir = tree.transform.forward;

        float degrees = Vector3.Angle(targetDir, dir);
        return degrees <= acceptableDegrees;
    }

    protected override void End()
    {
        tree.Blackboard.onBlackboardValueChange -= BlacKboardValueChanged;
        base.End();
    }
}
