using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compositor : BT_Node
{
    LinkedList<BT_Node> children = new LinkedList<BT_Node>();
    LinkedListNode<BT_Node> currentChild = null;

    public void AddChild(BT_Node newChild)
    {
        children.AddLast(newChild);
    }

    protected override NodeResult Execute()
    {
        if (children.Count == 0)
        {
            return NodeResult.Sucess;
        }

        currentChild = children.First;
        return NodeResult.InProgress;
    }

    protected BT_Node GetCurrentChild()
    {
        return currentChild.Value;
    }

    protected bool Next()
    {
        if (currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }
        return false;
    }

    protected override void End()
    {
        if (currentChild == null)
            return;

        currentChild.Value.Abort();
        currentChild = null;
    }

    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);

        foreach (var child in children)
        {
            child.SortPriority(ref priorityConter);
        }
    }

    public override BT_Node Get()
    {
        if (currentChild == null)
        {
            if (children.Count != 0)
            {
                return children.First.Value.Get();
            }
            else
            {
                return this;
            }
        }

        return currentChild.Value.Get();
    }
}
