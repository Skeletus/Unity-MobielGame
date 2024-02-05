using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_GetNextPatrolPoint : BT_Node
{
    PatrollingComponent patrollingComp;
    BehaviorTree tree;
    string patrolPointKey;
    public BT_Task_GetNextPatrolPoint(BehaviorTree tree, string patrolPointKey)
    {
        patrollingComp = tree.GetComponent<PatrollingComponent>();
        this.tree = tree;
        this.patrolPointKey = patrolPointKey;
    }

    protected override NodeResult Execute()
    {
        if (patrollingComp != null && patrollingComp.GetNextPatrolPoint(out Vector3 point))
        {
            tree.Blackboard.SetOrAddData(patrolPointKey, point);
            return NodeResult.Sucess;
        }

        return NodeResult.Failure;
    }
}
