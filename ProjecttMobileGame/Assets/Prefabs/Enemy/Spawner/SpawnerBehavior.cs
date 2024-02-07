using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : BehaviorTree
{
    protected override void ConstructTree(out BT_Node rootNode)
    {
        BT_Task_Spawn spawnTask = new BT_Task_Spawn(this);
        CooldownDecorator spawnCooldownDeco = new CooldownDecorator(this, spawnTask, 2f);
        BlackboardDecorator spawnBBDecorator = new BlackboardDecorator(this, spawnCooldownDeco, "Target", BlackboardDecorator.RunCondition.KeyExists, BlackboardDecorator.NotifyRule.RunConditionChange, BlackboardDecorator.NotifyAbort.both);

        rootNode = spawnBBDecorator;
    }
}
