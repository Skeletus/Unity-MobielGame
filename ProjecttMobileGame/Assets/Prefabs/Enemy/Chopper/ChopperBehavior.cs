using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BT_Node rootNode)
    {
        Selector RootSelector = new Selector();

        RootSelector.AddChild(new BT_TaskGroup_AttackTarget(this, 2, 10f));

        RootSelector.AddChild(new BT_TaskGroup_MoveToLastSeenLoc(this, 3));

        RootSelector.AddChild(new BT_TaskGroup_Patrolling(this, 3));

        rootNode = RootSelector;
    }

}