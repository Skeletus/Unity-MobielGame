using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : BT_Node
{
    BT_Node child;

    protected BT_Node GetChild()
    {
        return child;
    }

    public Decorator(BT_Node child)
    {
        this.child = child;
    }

    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        child.SortPriority(ref priorityConter);
    }

    public override BT_Node Get()
    {
        return child.Get();
    }
}
