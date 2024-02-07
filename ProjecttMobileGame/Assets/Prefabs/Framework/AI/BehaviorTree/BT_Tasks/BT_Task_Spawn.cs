using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Task_Spawn : BT_Node
{
    SpawnComponent spawnComponent;
    public BT_Task_Spawn(BehaviorTree tree)
    {
        spawnComponent = tree.GetComponent<SpawnComponent>();
    }
    protected override NodeResult Execute()
    {
        if (spawnComponent == null || !spawnComponent.StartSpawn())
        {
            return NodeResult.Failure;
        }

        return NodeResult.Sucess;
    }
}
