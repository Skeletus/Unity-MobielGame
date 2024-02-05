using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BT_Node rootNode)
    {
        Selector RootSelector = new Selector();

        #region attackTarget
        Sequencer attackTargetSeq = new Sequencer();
        BT_Task_MoveToTarget moveToTarget = new BT_Task_MoveToTarget(this, "Target", 2);


        BT_Task_RotateTowardsTarget rotateTowardsTarget = new BT_Task_RotateTowardsTarget(this, "Target", 10f);
        //attack
        BT_Task_AttackTarget attackTarget = new BT_Task_AttackTarget(this, "Target");

        attackTargetSeq.AddChild(moveToTarget);
        attackTargetSeq.AddChild(rotateTowardsTarget);
        attackTargetSeq.AddChild(attackTarget);

        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
                                                                            attackTargetSeq,
                                                                            "Target",
                                                                            BlackboardDecorator.RunCondition.KeyExists,
                                                                            BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                            BlackboardDecorator.NotifyAbort.both
                                                                            );

        RootSelector.AddChild(attackTargetDecorator);
        #endregion attackTarget

        #region CheckLastSeenLoc
        Sequencer CheckLastSeenLocSeq = new Sequencer();
        BT_Task_MoveToLoc MoveToLastSeenLoc = new BT_Task_MoveToLoc(this, "LastSeenLoc", 3);
        BT_Task_Wait WaitAtLastSeenLoc = new BT_Task_Wait(2f);
        BT_Task_RemoveBlackboard_Data removeLastSeenLoc = new BT_Task_RemoveBlackboard_Data(this, "LastSeenLoc");
        CheckLastSeenLocSeq.AddChild(MoveToLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(WaitAtLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(removeLastSeenLoc);

        BlackboardDecorator CheckLastSeenLocDeocorator = new BlackboardDecorator(this,
                                                                                 CheckLastSeenLocSeq,
                                                                                 "LastSeenLoc",
                                                                                 BlackboardDecorator.RunCondition.KeyExists,
                                                                                 BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                 BlackboardDecorator.NotifyAbort.none
                                                                                 );

        RootSelector.AddChild(CheckLastSeenLocDeocorator);

        #endregion CheckLastSeenLoc

        #region Patrolling
        Sequencer patrollingSeq = new Sequencer();

        BT_Task_GetNextPatrolPoint getNextPatrolPoint = new BT_Task_GetNextPatrolPoint(this, "PatrolPoint");
        BT_Task_MoveToLoc moveToPatrolPoint = new BT_Task_MoveToLoc(this, "PatrolPoint", 3);
        BT_Task_Wait waitAtPatrolPoint = new BT_Task_Wait(2f);

        patrollingSeq.AddChild(getNextPatrolPoint);
        patrollingSeq.AddChild(moveToPatrolPoint);
        patrollingSeq.AddChild(waitAtPatrolPoint);

        RootSelector.AddChild(patrollingSeq);

        #endregion Patrolling

        rootNode = RootSelector;
    }

}
