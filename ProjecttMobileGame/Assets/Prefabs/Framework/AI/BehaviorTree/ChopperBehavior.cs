using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BT_Node rootNode)
    {
        Selector RootSelector = new Selector();
        Sequencer attackTargetSeq = new Sequencer();
        BT_Task_MoveToTarget moveToTarget = new BT_Task_MoveToTarget(this, "Target", 2);
        attackTargetSeq.AddChild(moveToTarget);

        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
                                                                            attackTargetSeq, "Target",
                                                                            BlackboardDecorator.RunCondition.KeyExists,
                                                                            BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                            BlackboardDecorator.NotifyAbort.both
                                                                            );

        RootSelector.AddChild(attackTargetDecorator);

        Sequencer patrollingSeq = new Sequencer();

        BT_Task_GetNextPatrolPoint getNextPatrolPoint = new BT_Task_GetNextPatrolPoint(this, "PatrolPoint");
        BT_Task_MoveToLoc moveToPatrolPoint = new BT_Task_MoveToLoc(this, "PatrolPoint", 3);
        BT_Task_Wait waitAtPatrolPoint = new BT_Task_Wait(2f);

        patrollingSeq.AddChild(getNextPatrolPoint);
        patrollingSeq.AddChild(moveToPatrolPoint);
        patrollingSeq.AddChild(waitAtPatrolPoint);

        RootSelector.AddChild(patrollingSeq);

        rootNode = RootSelector;
    }

}
