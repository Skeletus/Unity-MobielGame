using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BT_Task_Group : BT_Node
{
    BT_Node Root;
    protected BehaviorTree tree;

    public BT_Task_Group(BehaviorTree tree)
    {
        this.tree = tree;
    }

    protected abstract void ConstructTree(out BT_Node Root);

    protected override NodeResult Execute()
    {
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        return Root.UpdateNode();
    }

    protected override void End()
    {
        Root.Abort();
        base.End();
    }

    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        Root.SortPriority(ref priorityConter);
    }

    public override void Initialize()
    {
        base.Initialize();
        ConstructTree(out Root);
    }

    public override BT_Node Get()
    {
        return Root.Get();
    }
}
