using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compositor : BT_Node
{
    LinkedList<BT_Node> children = new LinkedList<BT_Node>();
    LinkedListNode<BT_Node> currentChild = null;

    protected override NodeResult Execute()
    {
        if (children.Count == 0)
        {
            return NodeResult.Sucess;
        }

        currentChild = children.First;
        return NodeResult.InProgress;
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
        currentChild = null;
    }
}