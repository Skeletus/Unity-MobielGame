using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_TaskGroup_Patrolling : BT_Task_Group
{
    float acceptableDistance;
    public BT_TaskGroup_Patrolling(BehaviorTree tree, float acceptableDistance = 3) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BT_Node Root)
    {
        Sequencer patrollingSeq = new Sequencer();

        BT_Task_GetNextPatrolPoint getNextPatrolPoint = new BT_Task_GetNextPatrolPoint(tree, "PatrolPoint");
        BT_Task_MoveToLoc moveToPatrolPoint = new BT_Task_MoveToLoc(tree, "PatrolPoint", acceptableDistance);
        BT_Task_Wait waitAtPatrolPoint = new BT_Task_Wait(2f);

        patrollingSeq.AddChild(getNextPatrolPoint);
        patrollingSeq.AddChild(moveToPatrolPoint);
        patrollingSeq.AddChild(waitAtPatrolPoint);

        Root = patrollingSeq;
    }
}
