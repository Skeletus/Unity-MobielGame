using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BT_Node rootNode)
    {
        /*
        BT_Task_Wait waitTask = new BT_Task_Wait(2f);
        BT_Task_Log log = new BT_Task_Log("logging");
        BT_Task_AlwaysFail fail = new BT_Task_AlwaysFail();

        Sequencer Root = new Sequencer();

        Root.AddChild(log);
        Root.AddChild(waitTask);
        Root.AddChild(fail);

        rootNode = Root;
        */
        Sequencer patrollingSeq = new Sequencer();

        BT_Task_GetNextPatrolPoint getNextPatrolPoint = new BT_Task_GetNextPatrolPoint(this, "PatrolPoint");
        BT_Task_MoveToLoc moveToPatrolPoint = new BT_Task_MoveToLoc(this, "PatrolPoint", 3);
        BT_Task_Wait waitAtPatrolPoint = new BT_Task_Wait(2f);

        patrollingSeq.AddChild(getNextPatrolPoint);
        patrollingSeq.AddChild(moveToPatrolPoint);
        patrollingSeq.AddChild(waitAtPatrolPoint);

        rootNode = patrollingSeq;
    }

}
