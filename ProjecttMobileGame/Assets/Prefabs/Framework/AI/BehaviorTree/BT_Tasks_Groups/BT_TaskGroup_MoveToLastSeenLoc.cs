using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_TaskGroup_MoveToLastSeenLoc : BT_Task_Group
{
    float acceptableDistance;
    public BT_TaskGroup_MoveToLastSeenLoc(BehaviorTree tree, float acceptableDistance = 3) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BT_Node Root)
    {
        Sequencer CheckLastSeenLocSeq = new Sequencer();
        BT_Task_MoveToLoc MoveToLastSeenLoc = new BT_Task_MoveToLoc(tree, "LastSeenLoc", acceptableDistance);
        BT_Task_Wait WaitAtLastSeenLoc = new BT_Task_Wait(2f);
        BT_Task_RemoveBlackboard_Data removeLastSeenLoc = new BT_Task_RemoveBlackboard_Data(tree, "LastSeenLoc");
        CheckLastSeenLocSeq.AddChild(MoveToLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(WaitAtLastSeenLoc);
        CheckLastSeenLocSeq.AddChild(removeLastSeenLoc);

        BlackboardDecorator CheckLastSeenLocDeocorator = new BlackboardDecorator(tree,
                                                                                 CheckLastSeenLocSeq,
                                                                                 "LastSeenLoc",
                                                                                 BlackboardDecorator.RunCondition.KeyExists,
                                                                                 BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                 BlackboardDecorator.NotifyAbort.none
                                                                                 );

        Root = CheckLastSeenLocDeocorator;
    }
}
